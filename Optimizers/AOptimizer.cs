using System;
using System.Collections.Generic;
using MachineLearningCVRP.Evaluations;
using MachineLearningCVRP.StopConditions;
using MachineLearningCVRP.Utility;

namespace MachineLearningCVRP.Optimizers
{
    abstract class AOptimizer<Element>
    {
        public OptimizationResult<Element> Result { get; protected set; }

        public AEvaluation<Element> Evaluation { get; protected set; }

        public AStopCondition StopCondition { get; protected set; }

        public List<double> bestSolutions { get; protected set; }

        private long iterationNumber;
        private DateTime startTime;
        public double timeTaken { get; protected set; }

        public AOptimizer(AEvaluation<Element> evaluation, AStopCondition stopCondition)
        {
            Result = null;
            this.Evaluation = evaluation;
            this.StopCondition = stopCondition;
            bestSolutions = new List<double>();
        }

        public void Initialize()
        {
            Result = null;
            iterationNumber = 0;
            startTime = DateTime.UtcNow;
            timeTaken = 0;
            Initialize(startTime);
        }

        public bool RunIteration()
        {
            return RunIteration(iterationNumber++, startTime);
        }

        public bool ShouldStop()
        {
            return StopCondition.Stop((Result != null) ? Result.BestValue : double.NegativeInfinity, iterationNumber, Evaluation.iFFE, startTime);
        }


        public virtual void Run()
        {
            Initialize();

            while (!ShouldStop())
            {
                RunIteration();
                bestSolutions.Add(Result.BestValue);
            }
            var x = DateTime.UtcNow;
            timeTaken = (x - startTime).TotalSeconds;
        }

        protected abstract void Initialize(DateTime startTime);
        protected abstract bool RunIteration(long itertionNumber, DateTime startTime);

        // Minimizing cost
        protected bool CheckNewBest(List<Element> solution, double value, bool onlyImprovements = true)
        {
            if (Result == null || value < Result.BestValue || value == Result.BestValue && !onlyImprovements)
            {
                Result = new OptimizationResult<Element>(value, solution, iterationNumber, Evaluation.iFFE, TimeUtils.DurationInSeconds(startTime));

                return true;
            }

            return false;
        }
    }
}
