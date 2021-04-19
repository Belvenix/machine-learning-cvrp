using MachineLearningCVRP.Evaluations;
using MachineLearningCVRP.Mutations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Optimizers
{
    class Individual<Element>
    {
        private bool evaluated;

        public List<Element> Genotype { get; }
        public double Fitness { get; private set; }

        public Individual(List<Element> genotype)
        {
            evaluated = false;

            Genotype = genotype;
            Fitness = double.PositiveInfinity;
        }

        public Individual(Individual<Element> other)
        {
            evaluated = other.evaluated;

            Genotype = new List<Element>(other.Genotype);
            Fitness = other.Fitness;
        }

        public double Evaluate(AEvaluation<Element> evaluation)
        {
            if (!evaluated)
            {
                Fitness = evaluation.dEvaluate(Genotype);
                evaluated = true;
            }

            return Fitness;
        }

        public bool Mutate(IMutation<Element> mutation)
        {
            if (mutation.Mutate(Genotype))
            {
                evaluated = false;

                return true;
            }

            return false;
        }

        // True when this is better, False when other is better
        public bool BetterIndividual(Individual<Element> i2)
        {
            if (this.Fitness < i2.Fitness)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // True when this is better, False when other is better
        public bool BetterIndividual(double i2Fitness)
        {
            if (this.Fitness < i2Fitness)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
