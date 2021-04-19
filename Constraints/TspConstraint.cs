using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Constraints
{
    class TspConstraint : IConstraint<int>
    {
        public bool IsFeasible(List<int> solution)
        {
            return solution.Distinct().Count() == solution.Count();
        }
    }
}
