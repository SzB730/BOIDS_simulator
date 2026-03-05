using BoidsBuisnessLogic.Data;
using BoidsBuisnessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoidsBuisnessLogic.Simulation
{
    internal class Boid
    {
        private Vec2 _position;
        private Vec2 _velocity;

        public BoidCreateArgs Args { get; set; }

        public Boid(Vec2 pos, Vec2 vel, BoidCreateArgs args)
        {
            _position = pos;
            _velocity = vel;
            Args = args;
        }


        public Point GetPositionAsPoint()
        {
            return new Point() { X = _position.X, Y = _position.Y, BoidType = Args.BoidType };
        }

        public Vec2 GetDirection()
        {
            var lsq = _velocity.LengthSquared();
            return lsq > 1e-6f 
                ? _velocity / MathF.Sqrt(lsq) 
                : new Vec2(1, 0);
        }

        public Vec2 GetPosition()
        {
            return _position;
        }
    }
}
