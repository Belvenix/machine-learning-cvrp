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
    class SamplingOptimizer<Element> : AOptimizer<Element>
    {
        protected readonly AGenerator<Element> generator;

        public SamplingOptimizer(AEvaluation<Element> evaluation, AStopCondition stopCondition, AGenerator<Element> generator)
            : base(evaluation, stopCondition)
        {
            this.generator = generator;
        }

        protected override void Initialize(DateTime startTime)
        {
        }

        protected override bool RunIteration(long itertionNumber, DateTime startTime)
        {
            List<Element> solution = generator.Create(Evaluation.iSize);

            return CheckNewBest(solution, Evaluation.dEvaluate(solution));
        }
    }
}
