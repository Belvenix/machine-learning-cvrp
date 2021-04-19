using MachineLearningCVRP.Constraints;
using System.Collections.Generic;

namespace MachineLearningCVRP.Generators
{
    abstract class AGenerator<Element>
    {
        protected IConstraint<Element> pcConstraint;

        public AGenerator(IConstraint<Element> constraint)
        {
            pcConstraint = constraint;
        }

        public List<Element> Create(int size)
        {
            return Fill(size);
        }

        public abstract List<Element> Fill(int solution);
    }
}
