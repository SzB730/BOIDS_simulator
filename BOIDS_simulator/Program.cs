using BoidsBuisnessLogic.InfraStructure;
using BoidsBuisnessLogic.Samples;
using BoidsCore.Configuration;
using BoidsPresentation.Visualization;
using BoidsBuisnessLogic.Data;
using BoidsBuisnessLogic.Simulation;

namespace BoidsPresentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("BOIDS simulator");
            var simulationConfig = BoidsConfigReader.Load(@"C:\Users\bazsi\Documents\sze\dotnet\boidSimulator\BoidsConfig.json");
            Console.ReadLine();
            var drawer = new Drawer(simulationConfig);
            drawer.DrawEmpty();

            var previousPointsTemp = new List<PointOnScreen>();
            var boidsSimulator = new BoidsSimulator(simulationConfig, seed: 123);
            var deltaTime = 1f / 30f;
            var sleep = Convert.ToInt32(1000 * deltaTime / 2);
            for (int i = 0; i < 100000; i++)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);
                    switch (key.Key)
                    {
                        case ConsoleKey.E:
                            boidsSimulator.EmitNewBoid();
                            break;
                        case ConsoleKey.P:
                            boidsSimulator.EmitPredatorBoid();
                            break;
                        default:
                            break;
                    }
                }
                var points = boidsSimulator.GetBoidPositions();
                var minMaxValues = new MinMaxValueReader().Read(simulationConfig, points, isNoZoom: true);
                var pointsOnScreen = new PointsScaler().QuadrantScale(minMaxValues, points);
                drawer.DrawPointsOnScreen(previousPointsTemp, isClear: true);
                drawer.DrawQuadrantPointsOnScreen(pointsOnScreen);
                boidsSimulator.NextStep(deltaTime);
                //sleep
                previousPointsTemp.Clear();
                previousPointsTemp.AddRange(pointsOnScreen);
            }

            Console.SetCursorPosition(0, simulationConfig.DisplayHeight + 1);
            Console.WriteLine();
            Console.WriteLine("A kilepeshez nyomj egy entert");
            Console.ReadLine();
        }
    }
}