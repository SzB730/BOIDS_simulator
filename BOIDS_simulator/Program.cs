using BoidsBuisnessLogic.InfraStructure;
using BoidsBuisnessLogic.Samples;
using BoidsCore.Configuration;
using BoidsPresentation.Visualization;
using BoidsBuisnessLogic.Data;

namespace BoidsPresentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BOIDS simulator");
            var simulationConfig = BoidsConfigReader.Load(@"C:\Users\bazsi\Documents\programozascsharp\BoidsConfig.json");
            Console.ReadLine();
            var drawer = new Drawer(simulationConfig);
            drawer.DrawEmpty();

            var points = new SampleDataGenerator().GenerateSampleDataPoints();
            var minMaxValues = new MinMaxValueReader().Read(simulationConfig,points);



            /*

            /*
            Console.Write("Kerem a jatekter szelesseget: ");
            var widthString = Console.ReadLine();
            //var width = Convert.ToInt32(widthString);
            //var width = 0; // nem muszaj definialni a konverzioban is lehet out int width
            //var isWidthSuccess = Int32.TryParse(widthString, out width);
            
            var width = 0;
            var isWidthSuccess = false;
            
            do
            {
                Console.Write("Kerem a jatekter szelesseget");
                var widthString = Console.ReadLine();
                isWidthSuccess = Int32.TryParse(widthString, out width);
                isWidthSuccess = isWidthSuccess && width > 30 && width < 501;
            } while (!isWidthSuccess);

            var height = 0;
            var isHeightSuccess = false;

            do
            {
                Console.Write("Kerem a jatekter szelesseget");
                var heightString = Console.ReadLine();
                isHeightSuccess = Int32.TryParse(heightString, out height);
                isHeightSuccess = isHeightSuccess && height > 25 && height < 201;
            } while (!isHeightSuccess);

            var boidCount = 0;
            var isBoidCountSuccess = false;

            do
            {
                Console.Write("Kerem a jatekter szelesseget");
                var boidCountString = Console.ReadLine();
                isBoidCountSuccess = Int32.TryParse(boidCountString, out boidCount);
                isBoidCountSuccess = isBoidCountSuccess && boidCount > 0 && boidCount < 300;
            } while (!isBoidCountSuccess);

            BoidsSimulationConfig.Width = width;
            BoidsSimulationConfig.Height = height;
            BoidsSimulationConfig.BoidCount = boidCount;
             
            */

            Console.WriteLine();
            Console.WriteLine("A kilepeshez nyomj egy entert");
            Console.ReadLine();
        }
    }
}