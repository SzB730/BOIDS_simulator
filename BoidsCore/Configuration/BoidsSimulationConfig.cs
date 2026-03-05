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

        public float BoidMaxDeviancePercentage { get; set; }

        public float Speed {  get; set; }
        public float TurnRateRad { get; set; }
        public float VisionRadius { get; set; }

        public float Alignment { get; set; }
        public float Cohesion { get; set; }
        public float Separation { get; set; }

        public float EmitterXCoordinate { get; set; }
        public float EmitterYCoordinate { get; set; }

        public float PredatorXCoordinate { get; set; }
        public float PredatorYCoordinate { get; set; }

        public float PredatorSpeed { get; set; }
        public float PredatorTurnRateRad { get; set; }
        public float PredatorVisionRadius { get;set; }
        public float PredatorAlignment { get; set; }
        public float PredatorCohesion { get; set; }
        public float PredatorSeparation { get; set; }
    }
}
