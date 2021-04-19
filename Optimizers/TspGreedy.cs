using MachineLearningCVRP.Evaluations;
using MachineLearningCVRP.StopConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Optimizers
{
    class TspGreedy : TspRandomSearch
    {
        private readonly int startPosition;
        public TspGreedy(TspEvaluation evaluation, AStopCondition stopCondition, int start)
            : base(evaluation, stopCondition)
        {
            if (start < evaluation.iSize)
            {
                startPosition = start;
            }
            else
            {
                throw new Exception("incorrect starting position");
            }

        }
        protected override bool RunIteration(long itertionNumber, DateTime startTime)
        {
            List<int> possibleLocations = Enumerable.Range(0, Evaluation.iSize).ToList();
            List<int> greedySolution = new List<int>();
            var eval = ((TspEvaluation)Evaluation);
            
            for (int i = 0; i < Evaluation.iSize; i++)
            {
                if (i == 0)
                {
                    greedySolution.Add(startPosition);
                    possibleLocations.Remove(startPosition);
                }
                else
                {
                    int closestStation = eval.iSize;
                    foreach (int j in possibleLocations)
                    {
                        if(closestStation == eval.iSize)
                        {
                            closestStation = j;
                        }
                        else
                        {
                            var bestDistance = eval.distanceMatrix[greedySolution[i - 1], closestStation];
                            var curDistance = eval.distanceMatrix[greedySolution[i - 1], j];
                            if (curDistance == 0)
                            {
                                continue;
                            }
                            if (curDistance < bestDistance)
                            {
                                closestStation = j;
                            }
                        }
                    }
                    greedySolution.Add(closestStation);
                    possibleLocations.Remove(closestStation);
                }
                
            }
            if (eval.pcConstraint.IsFeasible(greedySolution))
            {
                return CheckNewBest(greedySolution, Evaluation.dEvaluate(greedySolution));
            }
            else
            {
                throw new Exception("Greedy destroys the solution!");
            }
        }
    }
}
