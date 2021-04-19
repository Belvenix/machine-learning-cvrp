using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Utility
{
    class UniformIntegerRandom : ASeedableRandom
    {
        public UniformIntegerRandom(int? seed = null)
            : base(seed)
        {

        }

        public int Next(int minValue, int maxValue)
        {
            return rng.Next(minValue, maxValue);
        }

        public int Next(int maxValue)
        {
            return rng.Next(maxValue);
        }

    }
}
