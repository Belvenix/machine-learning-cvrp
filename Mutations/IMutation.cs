using System.Collections.Generic;

namespace MachineLearningCVRP.Mutations
{
    interface IMutation<Element>
    {
        double Probability { get ; set; }

        bool Mutate(List<Element> solution);
    }
}
