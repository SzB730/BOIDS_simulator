using BoidsCore.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoidsBuisnessLogic.Data
{
    public class MinMaxValueReader
    {
        public MinMaxValues Read(BoidsSimulationConfig boidsSimulationConfig, List<Point> points)
        {
            var minMaxValues = new MinMaxValues()
            {
                DataMaxX = points.Select(p => p.X).Max(),
                DataMinX = points.Select(p => p.X).Min(),
                DataMaxY = points.Select(p => p.Y).Max(),
                DataMinY = points.Select(p => p.Y).Min(),

                DisplayMaxX = boidsSimulationConfig.Width-1,
                DisplayMinX = 0,
                DisplayMaxY = boidsSimulationConfig.Height-1,
                DisplayMinY = 0
            };

            return minMaxValues;
        }
    }
}
