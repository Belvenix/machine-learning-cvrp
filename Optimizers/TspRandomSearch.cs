using MachineLearningCVRP.Evaluations;
using MachineLearningCVRP.Generators;
using MachineLearningCVRP.StopConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Optimizers
{
    class TspRandomSearch : SamplingOptimizer<int>
    {
        public TspRandomSearch(TspEvaluation evaluation, AStopCondition stopCondition)
            : base(evaluation, stopCondition, new TspGenerator(new Random()))
        {
        }

        
    }
}
