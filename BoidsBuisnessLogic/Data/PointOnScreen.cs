using System;
using System.Collections.Generic;
using System.Text;

namespace BoidsBuisnessLogic.Data
{
    public struct PointOnScreen
    {
        public int X {  get; set; }
        public int Y { get; set; }

        public ConsoleColor Color { get; set; }

        public bool isBottomLeftQuadrant { get; set; }
        public bool isBottomRightQuadrant { get; set; }
        public bool isTopLeftQuadrant { get; set; }
        public bool isTopRightQuadrant { get; set; }

        public override string ToString()
        {
            return $"x:{X} \t y:{Y} \t BL: {(isBottomLeftQuadrant?"1":"0")} BR: {(isBottomRightQuadrant ? "1" : "0")} TL: {(isTopLeftQuadrant ? "1" : "0")} TR: {(isTopRightQuadrant ? "1" : "0")}";
        }
    }
}
