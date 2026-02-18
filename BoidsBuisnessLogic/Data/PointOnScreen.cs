using System;
using System.Collections.Generic;
using System.Text;

namespace BoidsBuisnessLogic.Data
{
    public struct PointOnScreen
    {
        public int X {  get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"x:{X} \t y:{Y}";
        }
    }
}
