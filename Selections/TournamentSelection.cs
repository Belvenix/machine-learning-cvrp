using System;
using System.Collections.Generic;
using MachineLearningCVRP.Optimizers;
using MachineLearningCVRP.Utility;

namespace MachineLearningCVRP.Selections
{
    sealed class TournamentSelection : ASelection
    {
        private readonly Random rng;

        private int size;
        private readonly double proportion;

        public TournamentSelection(int size, int? seed = null)
        {
            rng = new Random();

            this.size = size;
            proportion = -1;
        }

        public TournamentSelection(double proportion, int? seed = null)
        {
            rng = new Random();

            this.size = -1;
            if (proportion >= 0 && proportion <= 1)
            {
                this.proportion = proportion;
            }
            else
            {
                this.proportion = .05;
            }
        }

        protected override void AddToNewPopulation<Element>(List<Individual<Element>> population, List<Individual<Element>> newPopulation)
        {
            if (size == -1)
            {
                size = Convert.ToInt32(population.Count * proportion);
            }
            for(int i = 0; i < population.Count; ++i)
            {
                var indices = Shuffler.GenereteShuffledOrder(population.Count, new Random());
                newPopulation.Add(new Individual<Element>(TournamentWinner(population, indices)));
            }
        }

        private Individual<Element> TournamentWinner<Element>(List<Individual<Element>> population, List<int> indices)
        {
            Individual<Element> winner = population[indices[0]];
            for(int i = 1; i < size; ++i)
            {
                if (population[indices[i]].BetterIndividual(winner.Fitness))
                {
                    winner = population[indices[i]];
                }
            }

            return winner;
        }
    }
}
