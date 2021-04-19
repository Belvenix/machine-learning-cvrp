using MachineLearningCVRP.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Evaluations
{
    public class NullEvaluation<Element> : AEvaluation<Element>
    {
        public NullEvaluation(int iSize = 30)
        {
            this.iSize = iSize;
        }
        public override double dEvaluate(IList<Element> lSolution) 
        {
            return 0.0;
        }
    }
}
