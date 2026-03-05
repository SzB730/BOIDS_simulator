using System;
using System.Collections.Generic;
using System.Text;

namespace BoidsBuisnessLogic.Simulation
{
    public class BoidCreateArgs
    {
        public float Speed { get; set; }
        public float TurnRateRad { get; set; }
        public float VisionRadius { get; set; }

        public float Alignment { get; set; }
        public float Cohesion { get; set; }
        public float Separation { get; set; }

        public string BoidType { get; set; }
    }
}
