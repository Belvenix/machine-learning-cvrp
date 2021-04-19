using MachineLearningCVRP.Constraints;
using System.Collections.Generic;

namespace MachineLearningCVRP.Evaluations
{
    public abstract class AEvaluation<Element>
    {
        public long iFFE;
        public IConstraint<Element> pcConstraint;
        public int iSize;
        public abstract double dEvaluate(IList<Element> lSolution);
    }
}
