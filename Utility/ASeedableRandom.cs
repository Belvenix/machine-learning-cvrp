using System;

namespace MachineLearningCVRP.Utility
{
    class ASeedableRandom
    {
        protected readonly Random rng;

        public ASeedableRandom(int? seed = null)
        {
            rng = seed.HasValue ? new Random(seed.Value) : new Random();
        }
    }
}
