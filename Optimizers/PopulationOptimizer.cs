using MachineLearningCVRP.Evaluations;
using MachineLearningCVRP.Generators;
using MachineLearningCVRP.Mutations;
using MachineLearningCVRP.Selections;
using MachineLearningCVRP.StopConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Optimizers
{
    abstract class APopulationOptimizer<Element> : AOptimizer<Element>
    {
        protected readonly AGenerator<Element> generator;
        protected readonly ASelection selection;
        protected readonly IMutation<Element> mutation;

        protected readonly int populationSize;
        public List<Individual<Element>> population;

        public List<double> averageValues;
        public List<double> worstValues;

        public APopulationOptimizer(AEvaluation<Element> evaluation, AStopCondition stopCondition, AGenerator<Element> generator,
                                    ASelection selection, IMutation<Element> mutation, int populationSize)
            : base(evaluation, stopCondition)
        {
            this.generator = generator;
            this.selection = selection;
            this.mutation = mutation;

            this.populationSize = populationSize;
            population = new List<Individual<Element>>();

            averageValues = new List<double>();
            worstValues = new List<double>();
        }

        protected override sealed void Initialize(DateTime startTime)
        {
            population.Clear();
            for (int i = 0; i < populationSize; ++i)
            {
                population.Add(CreateIndividual());
            }

            Evaluate();
            CheckNewBest();
        }

        protected void Evaluate()
        {
            foreach (Individual<Element> individual in population)
            {
                individual.Evaluate(Evaluation);
            }
        }

        protected void AnalizePopulation()
        {
            double sum = 0.0;
            double worst = Double.NegativeInfinity;
            foreach (var individual in population)
            {
                sum += individual.Fitness;
                if (!individual.BetterIndividual(worst))
                {
                    worst = individual.Fitness;
                }
            }
            sum /= populationSize;
            averageValues.Add(sum);
            worstValues.Add(worst);
        }

        protected void Select()
        {
            selection.Select(ref population);
        }

        protected void Mutate()
        {
            foreach (Individual<Element> individual in population)
            {
                individual.Mutate(mutation);
            }
        }

        protected bool CheckNewBest(bool onlyImprovements = true)
        {
            Individual<Element> bestInPopulation = population[0];
            for (int i = 1; i < population.Count; ++i)
            {
                if (population[i].BetterIndividual(bestInPopulation))
                {
                    bestInPopulation = population[i];
                }
            }

            return CheckNewBest(bestInPopulation.Genotype, bestInPopulation.Fitness, onlyImprovements);
        }

        protected Individual<Element> CreateIndividual(List<Element> genotype = null)
        {
            if (genotype == null)
            {
                genotype = generator.Create(Evaluation.iSize);
            }

            return new Individual<Element>(genotype);
        }
    }
}
