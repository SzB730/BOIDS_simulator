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

        public BoidsSimulator(BoidsSimulationConfig config, int? seed = null)
        {
            _config = config;
            _rnd = seed.HasValue ? new Random(seed.Value) : new Random();
            _boids = new Boid[_config.BoidCount];

            Initalize();
        }

        public List<Point> GetBoidPositions()
        {
            return _boids.Select(b => b.GetPositionAsPoint()).ToList();
        }

        public void NextStep()
        {

        }

        private void Initalize()
        {
            for (int i = 0; i < _boids.Length; i++)
            {
                var x = _rnd.NextSingle() * _config.SimulationWidth;
                var y = _rnd.NextSingle() * _config.SimulationHeight;

                var a = _rnd.NextSingle() * 2f * MathF.PI;

                var dir = new Vec2(MathF.Cos(a), MathF.Sin(a));
                var vel = dir * _config.Speed;

                _boids [i] = new Boid(new Vec2(x, y), vel);
            }
        }
    }
}
