using BoidsCore.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoidsBuisnessLogic.Data
{
    public class MinMaxValueReader
    {
        public MinMaxValues Read(BoidsSimulationConfig boidsSimulationConfig, List<Point> points, bool isNoZoom = false)
        {
            var minMaxValues = new MinMaxValues()
            {
                DataMaxX = isNoZoom ? boidsSimulationConfig.SimulationWidth : points.Select(p => p.X).Max(),
                DataMinX = isNoZoom ? 0 : points.Select(p => p.X).Min(),
                DataMaxY = isNoZoom ? boidsSimulationConfig.SimulationHeight : points.Select(p => p.Y).Max(),
                DataMinY = isNoZoom ? 0 : points.Select(p => p.Y).Min(),

                DisplayMaxX = boidsSimulationConfig.DisplayWidth-1,
                DisplayMinX = 0,
                DisplayMaxY = boidsSimulationConfig.DisplayHeight-1,
                DisplayMinY = 0
            };

            return minMaxValues;
        }
    }
}
