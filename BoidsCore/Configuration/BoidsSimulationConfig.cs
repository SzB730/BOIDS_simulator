using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BoidsCore.Configuration
{
    public class BoidsSimulationConfig
    {
        public int DisplayWidth { get; set; }
        public int DisplayHeight { get; set; }

        public float SimulationWidth { get; set; }
        public float SimulationHeight { get; set; }
        public int BoidCount { get; set; }

        public float Speed {  get; set; }
        public float TurnRateRad { get; set; }
        public float VisionRadius { get; set; }

        public float Alignment { get; set; }
        public float Cohesion { get; set; }
        public float Separation { get; set; }
    }
}
