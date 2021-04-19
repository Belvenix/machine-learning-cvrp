using System;
using MachineLearningCVRP.Optimizers;
using MachineLearningCVRP.Evaluations;
using MachineLearningCVRP.StopConditions;
using System.Collections.Generic;
using System.Linq;
using MachineLearningCVRP.Crossovers;
using MachineLearningCVRP.Generators;
using MachineLearningCVRP.Selections;
using MachineLearningCVRP.Mutations;
using System.IO;

namespace MachineLearningCVRP
{
    class Program
    {

        private static void ParameterTestGA(List<TspEvaluation> problems)
        {
            double[] crossoverProbs = { 0, 0.01, 0.1, 0.2, 0.5, 0.75, 1 };
            double[] mutationProbs = { 0, 0.01, 0.05, 0.1, 0.2, 0.5 };
            int[] popSize = {10, 50, 100, 200, 500, 1000 };
            int[] generations = {10, 100, 1000, 10000 };

            double defaultCrossover = .7, defaultMutation = .1;
            int defaultPopSize = 100, defaultIterations = 100, defaultTournament = 5;

            double[] tournamentProportions = { .05, .1, .25, .5 };

            int[] tournamentCount = { 1, 3, 5, 10 };

            Console.WriteLine("Checking crossover");

            foreach (var probCrossover in crossoverProbs)
            {
                foreach (var problem in problems)
                {
                    TestGA(problem, probCrossover, defaultMutation, defaultPopSize, defaultIterations, defaultTournament);
                }
            }

            Console.WriteLine("Checking mutation");
            foreach (var probMutation in mutationProbs)
            {
                foreach (var problem in problems)
                {
                    TestGA(problem, defaultCrossover, probMutation, defaultPopSize, defaultIterations, defaultTournament);
                }
            }

            Console.WriteLine("Checking population");
            foreach (var size in popSize)
            {
                Console.WriteLine("Checking population with value: " + size.ToString());
                foreach (var problem in problems)
                {
                    TestGA(problem, defaultCrossover, defaultMutation, size, defaultIterations, defaultTournament);
                }
            }

            Console.WriteLine("Checking iterations");
            foreach (var iterations in generations)
            {
                Console.WriteLine("Checking iterations with value: " + iterations.ToString());
                foreach (var problem in problems)
                {
                    TestGA(problem, defaultCrossover, defaultMutation, defaultPopSize, iterations, defaultTournament);
                }
            }

            Console.WriteLine("Checking tournament");
            foreach (var tournament in tournamentProportions)
            {
                foreach (var problem in problems)
                {
                    TestGA(problem, defaultCrossover, defaultMutation, defaultPopSize, defaultIterations, tournament);
                }
            }

        }

        private static void TestGA(AEvaluation<int> problem, double probCrossover, double probMutation, int population, int iterations, double tournament)
        {
            List<String> results = new List<String>();
            List<String> learningProgress = new List<String>();
            int savedRun = new Random().Next(0, 10);
            for (int i = 0; i < 10; i++)
            {
                //var evaluation = new TspEvaluation(@"C:\Users\jbelter\source\repos\machine-learning-cvrp\data\A-n32-k5.vrp");
                var evaluation = problem;
                var stopCondition = new IterationsStopCondition(iterations);
                //var optimizer = new TspRandomSearch(evaluation, stopCondition);
                var generator = new TspGenerator(new Random());
                ASelection selection;
                if (tournament > 1 && tournament <= population)
                {
                    selection = new TournamentSelection(Convert.ToInt32(tournament));
                }
                else if (tournament > 0 && tournament <= 1)
                {
                    selection = new TournamentSelection(tournament);
                }
                else
                {
                    selection = new TournamentSelection(5);
                }
                
                var crossover = new OrderedCrossover(probCrossover);
                var mutation = new SwapMutation(probMutation);
                var optimizer = new GeneticAlgorithm<int>(evaluation, stopCondition, generator, selection, crossover, mutation, population);

                optimizer.Run();

                if (i == savedRun)
                {
                    learningProgress = formatLearning(optimizer.worstValues, optimizer.averageValues, optimizer.bestSolutions);
                }

                //ReportOptimizationResult(optimizer.Result);
                results.Add(String.Join(", ",
                    problem.iSize.ToString(),
                    optimizer.timeTaken.ToString(),
                    optimizer.bestSolutions.Last().ToString(),
                    optimizer.averageValues.Last().ToString(),
                    optimizer.worstValues.Last().ToString(),
                    probCrossover.ToString(),
                    probMutation.ToString(),
                    population.ToString(),
                    iterations.ToString(),
                    tournament.ToString()
                ));
            }    
            SaveToFile(@"C:\Users\jbelter\source\repos\machine-learning-cvrp\resultsGA.txt", results);
            SaveToFile((@"C:\Users\jbelter\source\repos\machine-learning-cvrp\progress\GA" + "-" +
                    problem.iSize.ToString() + "-" + 
                    probCrossover.ToString() + "-" +
                    probMutation.ToString() + "-" +
                    population.ToString() + "-" +
                    iterations.ToString() + "-" +
                    tournament.ToString())
                    .Replace(".", "") + ".txt",
                learningProgress);
            
        }

        private static List<String> formatLearning(List<double> worst, List<double> avg, List<double> best)
        {
            List<String> result = new List<string>();
            for (int i = 0; i < worst.Count; i++)
            {
                result.Add(String.Join(", ",
                    i.ToString(),
                    worst[i].ToString(),
                    avg[i].ToString(),
                    best[i].ToString()
                    ));
            }
            return result;
        }

        
        

        private static void TestGreedy(List<TspEvaluation> problems)
        {
            foreach (var problem in problems)
            {
                List<String> results = new List<String>();

                for (int i = 0; i < problem.iSize; i++)
                {
                    var evaluation = problem;
                    var stopCondition = new IterationsStopCondition(1);
                    var optimizer = new TspGreedy(evaluation, stopCondition, i);

                    optimizer.Run();

                    results.Add(String.Join(", ",
                        problem.iSize.ToString(),
                        optimizer.Result.BestValue.ToString(),
                        optimizer.timeTaken.ToString()
                        ));
                }
                SaveToFile(@"C:\Users\jbelter\source\repos\machine-learning-cvrp\resultsGreedy.txt", results);
            }
        }

        private static void TestRandom(List<TspEvaluation> problems)
        {
            foreach (var problem in problems)
            {
                List<String> results = new List<String>();

                for (int i = 0; i < 100; i++)
                {
                    var evaluation = problem;
                    var stopCondition = new IterationsStopCondition(100);
                    var optimizer = new TspRandomSearch(evaluation, stopCondition);

                    optimizer.Run();

                    results.Add(String.Join(", ",
                        problem.iSize.ToString(),
                        optimizer.Result.BestValue.ToString(),
                        optimizer.timeTaken.ToString()
                        ));
                }
                SaveToFile(@"C:\Users\jbelter\source\repos\machine-learning-cvrp\resultsRandom.txt", results);
            }
        }

        private static void TestAll()
        {
            List<TspEvaluation> problems = new List<TspEvaluation>();

            problems.Add(new TspEvaluation(@"C:\Users\jbelter\source\repos\machine-learning-cvrp\data\A-n32-k5.vrp"));
            problems.Add(new TspEvaluation(@"C:\Users\jbelter\source\repos\machine-learning-cvrp\data\A-n46-k7.vrp"));
            problems.Add(new TspEvaluation(@"C:\Users\jbelter\source\repos\machine-learning-cvrp\data\A-n61-k9.vrp"));

            //Console.WriteLine("Starting Greedy");
            //TestGreedy(problems);
            //Console.WriteLine("Starting Random");
            //TestRandom(problems);
            Console.WriteLine("Starting Genetic Algorithm");
            ParameterTestGA(problems);
            Console.WriteLine("Finished!");
        }

        private static void ReportOptimizationResult<Element>(OptimizationResult<Element> optimizationResult)
        {
            Console.WriteLine("value: {0}", optimizationResult.BestValue);
            Console.WriteLine("\twhen (time): {0}s", optimizationResult.BestTime);
            Console.WriteLine("\twhen (iteration): {0}", optimizationResult.BestIteration);
            Console.WriteLine("\twhen (FFE): {0}", optimizationResult.BestFFE);
            Console.WriteLine("\twhat (list): {0}", String.Join(",", optimizationResult.BestSolution));
        }

        static void Main(string[] args)
        {
            //TestAll();
            //TestCrossoverVsMutation()
            checkHugeProblem();

            Console.ReadLine();
        }

        private static void checkHugeProblem()
        {
            var hugeProblem = new TspEvaluation(@"C:\Users\jbelter\source\repos\machine-learning-cvrp\data\M-n200-k16.vrp");
            List<TspEvaluation> problem = new List<TspEvaluation>();
            problem.Add(hugeProblem);
            //TestRandom(problem);
            //TestGreedy(problem);
            TestGA(hugeProblem, .8, .4, 200, 500, 20);
        }

        private static void TestCrossoverVsMutation()
        {
            List<double> b = new List<double>();
            List<double> w = new List<double>();
            List<double> a = new List<double>();
            var evaluation = new TspEvaluation(@"C:\Users\jbelter\source\repos\machine-learning-cvrp\data\A-n46-k7.vrp");
            double[] px = { 0, .1, .8, 1, 0, 0, 0 };
            double[] pw = { 0, 0, 0, 0, .2, .5, 1 };
            for (int j = 0; j < 7; j++)
            {
                for (int i = 0; i < 20; i++)
                {

                    var stopCondition = new IterationsStopCondition(200);
                    //var optimizer = new TspRandomSearch(evaluation, stopCondition);
                    var generator = new TspGenerator(new Random());
                    ASelection selection = new TournamentSelection(Convert.ToInt32(5));

                    var crossover = new OrderedCrossover(px[j]);
                    var mutation = new SwapMutation(pw[j]);
                    var optimizer = new GeneticAlgorithm<int>(evaluation, stopCondition, generator, selection, crossover, mutation, 100);

                    optimizer.Run();

                    b.Add(optimizer.bestSolutions.Last());
                    w.Add(optimizer.worstValues.Last());
                    a.Add(optimizer.averageValues.Last());
                }
                double avgB = 0.0, avgA = 0.0, avgW = 0.0;
                for (int i = 0; i < b.Count; i++)
                {
                    avgB += b[i];
                    avgA += a[i];
                    avgW += w[i];
                }

                avgB /= b.Count;
                avgA /= b.Count;
                avgW /= b.Count;

                Console.WriteLine("avg(best value): " + avgB.ToString() +
                        " avg(average value): " + avgA.ToString() +
                        " avg(worst value): " + avgW.ToString());
            }
        }

        protected static void SaveToFile(String filepath, List<String> lines)
        {
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    foreach (var line in lines)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    foreach (var line in lines)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }
    }
}
