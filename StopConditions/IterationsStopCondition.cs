using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.StopConditions
{
    class IterationsStopCondition : AStopCondition
    {
        public readonly long maxIterationNumber;

        public IterationsStopCondition(long maxIterationNumber, double epsilon = double.Epsilon)
            : base(epsilon)
        {
            this.maxIterationNumber = maxIterationNumber;
        }

        protected override bool Stop(long iterationNumber, long FFE, DateTime startTime)
        {
            return iterationNumber >= maxIterationNumber;
        }
    }
}
