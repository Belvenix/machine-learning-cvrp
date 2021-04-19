using MachineLearningCVRP.Optimizers;
using System.Collections.Generic;

namespace MachineLearningCVRP.Selections
{
    abstract class ASelection
    {
        public void Select<Element>(ref List<Individual<Element>> population)
        {
            List<Individual<Element>> newPopulation = new List<Individual<Element>>(population.Count);

            AddToNewPopulation(population, newPopulation);
            population = newPopulation;
        }

        protected abstract void AddToNewPopulation<Element>(List<Individual<Element>> population, List<Individual<Element>> newPopulation);
    }
}
