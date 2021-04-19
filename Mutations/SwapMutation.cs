using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Mutations
{
    class SwapMutation : IMutation<int>
    {
        public SwapMutation(double probability)
        {
            Probability = probability;
        }
        public double Probability { get => prob;  set { prob = value; }   }

        private double prob;

        public bool Mutate(List<int> solution)
        {
            Random rnd = new Random();
            if (rnd.NextDouble() < Probability)
            {
                var f = rnd.Next(0, solution.Count);
                var s = rnd.Next(0, solution.Count);
                var tmp = solution[f];
                solution[f] = solution[s];
                solution[s] = tmp;
                return true;
            }
            return false; 
        }
    }
}
