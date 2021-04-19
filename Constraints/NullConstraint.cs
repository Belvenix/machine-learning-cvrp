using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Constraints
{
    class NullConstraint : IConstraint<int>
    {
        public bool IsFeasible(List<int> solution)
        {
            return true;
        }
    }
}
