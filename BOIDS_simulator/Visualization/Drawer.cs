using BoidsBuisnessLogic.Data;
using BoidsCore.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoidsPresentation.Visualization
{
    internal class Drawer
    {
        private int _width;
        private int _height;
        private int _boidCount;

        public Drawer(BoidsSimulationConfig boidsSimulationConfig)
        {
            _width = boidsSimulationConfig.DisplayWidth;
            _height = boidsSimulationConfig.DisplayHeight;
            _boidCount = boidsSimulationConfig.BoidCount;
        }

        public void DrawEmpty()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Green;
            for (int w = 0; w < _width; w++)
            {
                for (int h = 0; h < _height; h++)
                {
                    Console.SetCursorPosition(w, h);
                    Console.Write(" "); //█
                }
            }
        }

        public void DrawPointsOnScreen(List<PointOnScreen> pointsOnScreen, bool isClear = false)
        {
            foreach (var pointOnScreen in pointsOnScreen)
            {
                Console.SetCursorPosition(pointOnScreen.X, pointOnScreen.Y);
                Console.Write(isClear ? " " : "█");
            }
        }

        public void DrawQuadrantPointsOnScreen(List<PointOnScreen> pointsOnScreen)
        {
            foreach (var pointOnScreen in pointsOnScreen)
            {
                Console.ForegroundColor = pointOnScreen.Color;
                Console.SetCursorPosition(pointOnScreen.X, pointOnScreen.Y);
                if (pointOnScreen.isBottomLeftQuadrant && pointOnScreen.isBottomRightQuadrant && pointOnScreen.isTopLeftQuadrant && pointOnScreen.isTopRightQuadrant)
                {
                    Console.Write("█");
                }
                else if (pointOnScreen.isBottomLeftQuadrant && pointOnScreen.isBottomRightQuadrant && pointOnScreen.isTopLeftQuadrant)
                {
                    Console.Write("▙");
                }
                else if(pointOnScreen.isBottomLeftQuadrant && pointOnScreen.isBottomRightQuadrant && pointOnScreen.isTopRightQuadrant)
                {
                    Console.Write("▟");
                }
                else if (pointOnScreen.isBottomLeftQuadrant && pointOnScreen.isTopLeftQuadrant && pointOnScreen.isTopRightQuadrant)
                {
                    Console.Write("▛");
                }
                 else if (pointOnScreen.isBottomRightQuadrant && pointOnScreen.isTopLeftQuadrant && pointOnScreen.isTopRightQuadrant)
                {
                    Console.Write("▜");
                }
                else if (pointOnScreen.isBottomLeftQuadrant && pointOnScreen.isBottomRightQuadrant)
                {
                    Console.Write("▄");
                }
                else if (pointOnScreen.isBottomLeftQuadrant && pointOnScreen.isTopLeftQuadrant)
                {
                    Console.Write("▌");
                }
                else if (pointOnScreen.isBottomRightQuadrant && pointOnScreen.isTopLeftQuadrant)
                {
                    Console.Write("▚");
                }
                else if (pointOnScreen.isBottomLeftQuadrant && pointOnScreen.isTopRightQuadrant)
                {
                    Console.Write("▞");
                }
                else if (pointOnScreen.isBottomRightQuadrant && pointOnScreen.isTopRightQuadrant)
                {
                    Console.Write("▐");
                }
                else if (pointOnScreen.isTopLeftQuadrant && pointOnScreen.isTopRightQuadrant)
                {
                    Console.Write("▀");
                }
                else if (pointOnScreen.isBottomLeftQuadrant)
                {
                    Console.Write("▖");
                }
                else if (pointOnScreen.isBottomRightQuadrant)
                {
                    Console.Write("▗");
                }
                else if (pointOnScreen.isTopLeftQuadrant)
                {
                    Console.Write("▘");
                }
                else if (pointOnScreen.isTopRightQuadrant)
                {
                    Console.Write("▝");
                }
                else
                {
                    Console.Write(" ");

                }
            }
        }

    }
}
