using BoidsBuisnessLogic.Data;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;

namespace BoidsBuisnessLogic.Samples
{
    public class SampleDataGenerator
    {
        public List<Point> GenerateSampleDataPoints()
        {
            var result = new List<Point>();

            for (double x = 0; x < 1; x = x + 0.01)
            {
                result.Add(new Point()
                {
                    X = x,
                    Y = Math.Sin(x),
                });
            }
            
            return result;
        }
    }
}
