using BoidsBuisnessLogic.Data;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;

namespace BoidsBuisnessLogic.Samples
{
    public class SampleDataGenerator
    {
        public List<Point> GenerateSampleDataPoints(double i)
        {
            var result = new List<Point>();

            for (double x = 0; x < 8; x = x + 0.001)
            {
                result.Add(new Point()
                {
                    X = x,
                    Y = Math.Cos(x + i),
                });
            }
            
            return result;
        }
    }
}
