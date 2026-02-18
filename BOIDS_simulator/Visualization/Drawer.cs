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
            _width = boidsSimulationConfig.Width;
            _height = boidsSimulationConfig.Height;
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
                    Console.Write("█");
                }
            }
        }
    }
}
