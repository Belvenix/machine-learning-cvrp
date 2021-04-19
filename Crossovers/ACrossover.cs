using MachineLearningCVRP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Crossovers
{
    abstract class ACrossover
    {
        private readonly BoolRandom crossoverRNG;

        public double Probability { get; set; }

        public ACrossover(double probability, int? seed = null)
        {
            Probability = probability;

            crossoverRNG = new BoolRandom(seed);
        }

        public bool Crossover<Element>(List<Element> parent1, List<Element> parent2, List<Element> offspring1, List<Element> offspring2)
        {
            if (crossoverRNG.Next(Probability))
            {
                return Cross(parent1, parent2, offspring1, offspring2);
            }

            return false;
        }

        protected abstract bool Cross<Element>(List<Element> parent1, List<Element> parent2, List<Element> offspring1, List<Element> offspring2);
    }
}
