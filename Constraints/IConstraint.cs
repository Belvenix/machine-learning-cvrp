using System.Collections.Generic;

namespace MachineLearningCVRP.Constraints
{
    public interface IConstraint<Element>
    {
        bool IsFeasible(List<Element> solution);
    }
}
