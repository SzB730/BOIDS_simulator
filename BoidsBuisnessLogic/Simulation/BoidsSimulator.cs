using BoidsBuisnessLogic.Data;
using BoidsBuisnessLogic.Helpers;
using BoidsCore.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoidsBuisnessLogic.Simulation
{
    public class BoidsSimulator
    {
        private BoidsSimulationConfig _config;
        private Random _rnd;
        private Boid[] _boids;

        public BoidsSimulator(BoidsSimulationConfig config, int? seed = 123)
        {
            _config = config;
            _rnd = seed.HasValue
                ? new Random(seed.Value)
                : new Random();
            _boids = new Boid[_config.BoidCount];

            Initialize();
        }

        public List<Point> GetBoidPositions()
        {
            return _boids.Select(b => b.GetPositionAsPoint()).ToList();
        }

        public void NextStep(float deltaTime)
        {
            var next = new Boid[_boids.Length];
            for (int i = 0; i < _boids.Length; i++)
            {
                var b = _boids[i];
                var currentDirection = b.GetDirection();

                var alignmentSum = Vec2.Zero();
                var cohesionSum = Vec2.Zero();
                var separationSum = Vec2.Zero();
                var neighborCount = 0;

                for (int j = 0; j < _boids.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var other = _boids[j];
                    var delta = ToroidalDelta(b.GetPosition(), other.GetPosition(), _config.SimulationWidth, _config.SimulationHeight);
                    var distance = delta.Length();

                    if (distance <= b.Args.VisionRadius && distance > float.Epsilon)
                    {
                        neighborCount++;

                        alignmentSum += other.GetDirection();
                        cohesionSum += other.GetPosition();
                        var away = (-delta) / distance;
                        separationSum += away / (distance * distance);
                    }
                }
                Vec2 desiredDirection;
                if (neighborCount == 0)
                {
                    desiredDirection = currentDirection;
                }
                else
                {
                    var alignmentDirection = alignmentSum.NormalizeOrZero();

                    var center = cohesionSum / neighborCount;
                    var toCenter = ToroidalDelta(b.GetPosition(), center, _config.SimulationWidth, _config.SimulationHeight);
                    var cohesionDirection = toCenter.NormalizeOrZero();

                    var separationDirection = separationSum.NormalizeOrZero();

                    var steer = b.Args.Alignment * alignmentDirection
                        + b.Args.Cohesion * cohesionDirection
                        + b.Args.Separation * separationDirection;

                    desiredDirection = steer.LengthSquared() > float.Epsilon
                        ? steer.NormalizeOrZero()
                        : currentDirection;
                }

                var maxTurn = b.Args.TurnRateRad * deltaTime;
                var newDirection = Vec2.RotateTowards(currentDirection, desiredDirection, maxTurn);

                var newVelocity = newDirection * b.Args.Speed;
                var newPosition = b.GetPosition() + newVelocity * deltaTime;

                newPosition = Wrap(newPosition, _config.SimulationWidth, _config.SimulationHeight);

                next[i] = new Boid(newPosition, newVelocity, b.Args);
            }

            Array.Copy(next, _boids, _boids.Length);
        }

        private void Initialize()
        {
            for (int i = 0; i < _boids.Length; i++)
            {
                //var args = new BoidCreateArgs()
                //{
                //    Speed = _config.Speed +
                //            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.Speed),
                //    TurnRateRad = _config.TurnRateRad +
                //            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.TurnRateRad),
                //    VisionRadius = _config.VisionRadius +
                //            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.VisionRadius),
                //    Alignment = _config.Alignment +
                //            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.Alignment),
                //    Cohesion = _config.Cohesion +
                //            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.Cohesion),
                //    Separation = _config.Separation +
                //            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.Separation),
                //    BoidType = BoidTypes.NormalBoid
                //};

                //var x = _rnd.NextSingle() * _config.SimulationWidth;
                //var y = _rnd.NextSingle() * _config.SimulationHeight;

                //var a = _rnd.NextSingle() * 2f * MathF.PI;

                //var dir = new Vec2(MathF.Cos(a), MathF.Sin(a));
                //var vel = dir * _config.Speed;

                //_boids[i] = new Boid(new Vec2(x, y), vel, args);
                _boids[i] = GenerateBoid();
            }
        }

        private Boid GenerateBoid(float? xOverride = null, float? yOverride = null, string? boidType = null)
        {
            var args = new BoidCreateArgs()
            {
                Speed = boidType == BoidTypes.PredatorBoid ?_config.PredatorSpeed +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.PredatorSpeed)
                : _config.Speed +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.Speed),

                TurnRateRad = boidType == BoidTypes.PredatorBoid ? _config.PredatorTurnRateRad +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.PredatorTurnRateRad)
                : _config.TurnRateRad +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.TurnRateRad),

                VisionRadius = boidType == BoidTypes.PredatorBoid ? _config.PredatorVisionRadius +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.PredatorVisionRadius)
                : _config.VisionRadius +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.VisionRadius),

                Alignment = boidType == BoidTypes.PredatorBoid ? _config.PredatorAlignment +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.PredatorAlignment)
                : _config.Alignment +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.Alignment),

                Cohesion = boidType == BoidTypes.PredatorBoid ? _config.PredatorCohesion +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.PredatorCohesion)
                : _config.Cohesion +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.Cohesion),

                Separation = boidType == BoidTypes.PredatorBoid ? _config.PredatorSeparation +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.PredatorSeparation)
                : _config.Separation +
                            ((_config.BoidMaxDeviancePercentage - (_rnd.NextSingle() * _config.BoidMaxDeviancePercentage * 2f)) * _config.Separation),
                BoidType = boidType ?? BoidTypes.NormalBoid
            };

            var x = xOverride ?? _rnd.NextSingle() * _config.SimulationWidth;
            var y = yOverride ?? _rnd.NextSingle() * _config.SimulationHeight;

            var a = _rnd.NextSingle() * 2f * MathF.PI;

            var dir = new Vec2(MathF.Cos(a), MathF.Sin(a));
            var vel = dir * _config.Speed;

            return new Boid(new Vec2(x, y), vel, args);
        }

        public static Vec2 Wrap(Vec2 p, float w, float h)
        {
            var x = p.X % w;
            if (x < 0)
            {
                x += w;
            }

            var y = p.Y % h;
            if (y < 0)
            {
                y += h;
            }

            return new Vec2(x, y);
        }

        public (float X, float Y) GetCenterOfMass()
        {
            if (_boids.Length == 0)
            {
                return (0f, 0f);
            }

            Vec2 sum = Vec2.Zero();

            foreach (var b in _boids)
            {
                sum += b.GetPosition();
            }

            var c = sum / _boids.Length;

            return (c.X, c.Y);
        }

        private static Vec2 ToroidalDelta(Vec2 from, Vec2 to, float w, float h)
        {
            var dx = to.X - from.X;
            var dy = to.Y - from.Y;

            if (dx > w / 2f)
            {
                dx -= w;
            }

            if (dx < -w / 2f)
            {
                dx += w;
            }

            if (dy > h / 2f)
            {
                dy -= h;
            }

            if (dy < -h / 2f)
            {
                dy += h;
            }

            return new Vec2(dx, dy);
        }

        public void EmitNewBoid()
        {
            var next = new Boid[_boids.Length + 1];
            Array.Copy(_boids, next, _boids.Length);
            next[_boids.Length] = GenerateBoid(_config.EmitterXCoordinate, _config.EmitterYCoordinate, BoidTypes.EmittedBoid);
            _boids = next;
        }

        public void EmitPredatorBoid()
        {
            var next = new Boid[_boids.Length + 1];
            Array.Copy(_boids, next, _boids.Length);
            next[_boids.Length] = GenerateBoid(_config.PredatorXCoordinate, _config.PredatorYCoordinate, BoidTypes.PredatorBoid);
            _boids = next;
        }
    }
}
