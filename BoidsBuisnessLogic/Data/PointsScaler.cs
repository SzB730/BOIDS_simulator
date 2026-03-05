using BoidsBuisnessLogic.Simulation;
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
                y = Math.Round(y, 0, MidpointRounding.AwayFromZero);

                y = minMaxValues.DisplayMaxY - y;

                //integer convert
                var pointOnScreen = new PointOnScreen()
                {
                    X = Convert.ToInt32(x),
                    Y = Convert.ToInt32(y),
                };

                if (!result.Any(p => p.X == pointOnScreen.X && p.Y == pointOnScreen.Y))
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

        public List<PointOnScreen> QuadrantScale(MinMaxValues minMaxValues, List<Point> points)
        {
            var result = new List<PointOnScreen>();

            foreach (var point in points)
            {
                //normalizalas
                var x = Normalize(point.X, minMaxValues.DataMinX, minMaxValues.DataMaxX, minMaxValues.DisplayMinX, minMaxValues.DisplayMaxX);
                var y = Normalize(point.Y, minMaxValues.DataMinY, minMaxValues.DataMaxY, minMaxValues.DisplayMinY, minMaxValues.DisplayMaxY);

                //kerekit
                x = Math.Round(x, 2, MidpointRounding.AwayFromZero);
                y = Math.Round(y, 2, MidpointRounding.AwayFromZero);
                y = minMaxValues.DisplayMaxY - y;

                var xDecimal = x - Math.Truncate(x);
                var yDecimal = y - Math.Truncate(y);

                var color = GetColor(point);

                //integer convert
                var pointOnScreen = new PointOnScreen()
                {
                    X = Convert.ToInt32(Math.Truncate(x)),
                    Y = Convert.ToInt32(Math.Truncate(y)),

                    isBottomLeftQuadrant = xDecimal <= 0.5 && yDecimal > 0.5,
                    isBottomRightQuadrant = xDecimal > 0.5 && yDecimal > 0.5,
                    isTopLeftQuadrant = xDecimal <= 0.5 && yDecimal <= 0.5,
                    isTopRightQuadrant = xDecimal > 0.5 && yDecimal <= 0.5,

                    Color = color
                };

                if (!result.Any(p => p.X == pointOnScreen.X && p.Y == pointOnScreen.Y))
                {
                    result.Add(pointOnScreen);
                }
                else
                {
                    var item = result.FirstOrDefault(p => p.X == pointOnScreen.X && p.Y == pointOnScreen.Y);
                    result.Remove(item);

                    item.isBottomLeftQuadrant = item.isBottomLeftQuadrant || pointOnScreen.isBottomLeftQuadrant;
                    item.isBottomRightQuadrant = item.isBottomRightQuadrant || pointOnScreen.isBottomRightQuadrant;
                    item.isTopLeftQuadrant = item.isTopLeftQuadrant || pointOnScreen.isTopLeftQuadrant;
                    item.isTopRightQuadrant = item.isTopRightQuadrant || pointOnScreen.isTopRightQuadrant;

                    item.Color = ConsoleColor.White;

                    result.Add(item);
                }
            }

            return result;
        }

        private ConsoleColor GetColor(Point point)
        {
            if (point.BoidType == BoidTypes.NormalBoid)
            {
                return ConsoleColor.Green;
            }
            else if (point.BoidType == BoidTypes.EmittedBoid)
            {
                return ConsoleColor.Yellow;
            }
            else if (point.BoidType == BoidTypes.PredatorBoid)
            {
                return ConsoleColor.Red;
            }
            else
            {
                return ConsoleColor.Gray;
            }
        }
    }
}
