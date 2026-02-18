using System;
using System.Collections.Generic;
using System.Text;

namespace BoidsBuisnessLogic.Data
{
    public class PointsScaler
    {
        public List<PointOnScreen> Scale(MinMaxValues minMaxValues, List<Point> points)
        { 
            var result = new List<PointOnScreen>();

            foreach (var point in points)
            {
                //normalizalas
                var x = Normalize(point.X, minMaxValues.DataMinX, minMaxValues.DataMaxX, minMaxValues.DisplayMinX, minMaxValues.DisplayMaxX);
                var y = Normalize(point.Y, minMaxValues.DataMinY, minMaxValues.DataMaxY, minMaxValues.DisplayMinY, minMaxValues.DisplayMaxY);

                //kerekit
                x = Math.Round(x, 0, MidpointRounding.AwayFromZero);
                y = Math.Round(y, 0 , MidpointRounding.AwayFromZero);

                //integer convert
                var pointOnScreen = new PointOnScreen()
                {
                    X = Convert.ToInt32(x),
                    Y = Convert.ToInt32(y),
                };

                if (result.Any(p => p.X == pointOnScreen.X && p.Y == pointOnScreen.Y))
                {
                    result.Add(pointOnScreen);
                }
            }

            return result;
        }

        double Normalize(double val, double valmin, double valmax, double min, double max)
        {
            return (((val - valmin) / (valmax - valmin)) * (max - min)) + min;
        }

    }
}
