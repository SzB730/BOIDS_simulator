using BoidsCore.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoidsBuisnessLogic.InfraStructure
{
    public static class BoidsConfigReader
    {
        public static BoidsSimulationConfig Load(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    var json = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<BoidsSimulationConfig>(json);
                }
                catch (Exception ex)
                {
                    {
                        Console.WriteLine($"HIBA: Konfiguracio nem olvashato. (Hibauzenet: {ex.Message})");
                        throw;
                    }
                }
            }
            else
            {
                throw new Exception("Hiba: Konfiguracio nem talalhato.");
            }
        }
    }
}
