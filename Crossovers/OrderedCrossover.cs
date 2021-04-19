using System;
using System.Collections.Generic;
using MachineLearningCVRP.Utility;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Crossovers
{
    class OrderedCrossover : ACrossover
    {
        private readonly UniformIntegerRandom pointRnd;
        public OrderedCrossover(double probability, int? seed = null)
            : base(probability, seed)
        {
            pointRnd = new UniformIntegerRandom(seed);
        }
        protected override bool Cross<Element>(List<Element> parent1, List<Element> parent2, List<Element> offspring1, List<Element> offspring2)
        {
            OrderCross(parent1, parent2, offspring1);
            OrderCross(parent2, parent1, offspring2);
            return true;
        }

        public bool Test<Element>(List<Element> parent1, List<Element> parent2, List<Element> offspring1, List<Element> offspring2)
        {
            return Cross(parent1, parent2, offspring1, offspring2);
        }

        private void OrderCross<Element>(List<Element> parent1, List<Element> parent2, List<Element> offspring)
        {
            int firstPoint = pointRnd.Next(0, parent1.Count - 1);
            int secondPoint = pointRnd.Next(0, parent1.Count - 1);

            var start = firstPoint < secondPoint ? firstPoint : secondPoint;
            var end = firstPoint > secondPoint ? firstPoint : secondPoint;

            List<Element> saveSequence = new List<Element>();
            List<Element> restSequence = new List<Element>();
            for (int i = start; i < end; i++)
            {
                saveSequence.Add(parent1[i]);
            }

            for (int i = 0; i < parent2.Count; i++)
            {
                if (!saveSequence.Contains(parent2[i]))
                {
                    restSequence.Add(parent2[i]);
                }
            }

            for (int i = 0, s = 0, r = 0; i < offspring.Count; i++)
            {
                if (i >= start && i < end)
                {
                    offspring[i] = saveSequence[s++];
                }
                else
                {
                    offspring[i] = restSequence[r++];
                }
            }
        }
    }
}
