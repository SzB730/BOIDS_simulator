using System;
using System.Collections.Generic;
using System.Text;

namespace BoidsBuisnessLogic.Data
{
    public struct Point
    {
        public double X {  get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return $"x:{X} \t y:{Y}";
        }
    }
}
