using MachineLearningCVRP.Constraints;
using MachineLearningCVRP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Generators
{
    class TspGenerator : AGenerator<int>
    {
        private Random rnd;
        public TspGenerator(Random rnd)
            : base(new TspConstraint())
        {
            this.rnd = rnd;
        }

        public override List<int> Fill(int size)
        {
            List<int> solution;
            do
            {
                solution = Shuffler.GenereteShuffledOrder(size, rnd);
            } while (!pcConstraint.IsFeasible(solution));
            return Shuffler.GenereteShuffledOrder(size, rnd);
        }
    }
}
