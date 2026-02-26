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
        public Boid(Vec2 pos, Vec2 vel)
        {
            _position = pos;
            _velocity = vel;
        }

        public Point GetPositionAsPoint()
        {
            return new Point() { X = _position.X, Y = _position.Y };
        }
    }
}
