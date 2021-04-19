using MachineLearningCVRP.Constraints;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Evaluations
{
    public class TspEvaluation : AEvaluation<int>
    {
        public long[,] distanceMatrix { get; private set; }
        //public int capacity { get; private set; }
        public TspEvaluation(int iSize)
        {
            this.iSize = iSize;
            if (iSize == 3)
            {
                // (0, 2, 1) or (1, 0, 2)
                distanceMatrix = new long[,] { { 0, 4, 1 }, { 4, 0, 3 }, { 1, 3, 0 } };
            }
            else if (iSize == 5)
            {
                // (0, 1, 2, 3, 4 )
                distanceMatrix = new long[,] 
                {
                    { 0, 4, 5, 7, 6},
                    { 4, 0, 3, 6, 7},
                    { 5, 3, 0, 3, 5},
                    { 7, 6, 3, 0, 4},
                    { 6, 7, 5, 4, 0}
                };
                //9//------------------------------
                //8//------------------------------
                //7//------------------------------
                //6//-------2--------3--------4----
                //5//------------------------------
                //4//------------------------------
                //3//------------------------------
                //2//-------1-----------------5----
                //1//------------------------------
                //0//------------------------------
                //x///0//1//2//3//4//5//6//7//8//9/
            }
            else
            {
                throw new NotImplementedException();
            }
            this.pcConstraint = new TspConstraint();
        }

        public TspEvaluation(string filepath)
        {
            this.pcConstraint = new TspConstraint();
            CreateDistances(filepath);
        }

        public bool checkFeasibility(List<int> solution)
        {
            return pcConstraint.IsFeasible(solution);
        }

        public override double dEvaluate(IList<int> lSolution)
        {
            long sum = 0;
            for (int i = 0; i < lSolution.Count - 1; i++)
            {
                sum += distanceMatrix[lSolution[i], lSolution[i + 1]];
            }
            return sum;
        }

        public void CreateDistances(string filepath)
        {
            using (StreamReader input = new StreamReader(filepath))
            {
                string[] splitted;
                while (true)
                {
                    splitted = input.ReadLine().Split(':');
                    if (splitted[0].Contains("COMMENT"))
                    {
                        //string[] splittedComment = splitted[1].Split("Optimal value:");
                        //if (splittedComment.Length > 1)
                        //{
                        //    long Max = long.Parse(splittedComment[1].Replace(")",""));
                        //}
                    }
                    else if (splitted[0].Contains("DIMENSION"))
                    {
                        iSize = int.Parse(splitted[1]);
                    }
                    else if (splitted[0].Contains("CAPACITY"))
                    {
                        //capacity = int.Parse(splitted[1]);
                    }
                    else if (splitted[0].Contains("EDGE_WEIGHT_TYPE"))
                    {
                        if (!splitted[1].Trim().Equals("EUC_2D"))
                            throw new NotImplementedException("Edge Weight Type " + splitted[1] + " is not supported (only EUC_2D)");
                    }
                    else if (splitted[0].Contains("NODE_COORD_SECTION"))
                    {
                        break;
                    }
                }

                long[] nodeX = new long[iSize];
                long[] nodeY = new long[iSize];
                for (int n = 0; n < iSize; n++)
                {
                    splitted = input.ReadLine().Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    if (long.Parse(splitted[0]) != n + 1)
                    {
                        throw new Exception("Unexpected index");
                    }
                    else
                    {
                        nodeX[n] = long.Parse(splitted[1]);
                        nodeY[n] = long.Parse(splitted[2]);
                    }
                }

                ComputeDistanceMatrix(nodeX, nodeY);
            }
        }

        private void ComputeDistanceMatrix(long[] nodeX, long[] nodeY)
        {
            distanceMatrix = new long[iSize, iSize];
            for (int i = 0; i < iSize; i++)
            {
                for (int j = 0; j < iSize; j++)
                {
                    if (i != j)
                    {
                        long dist = ComputeDistance(nodeX[i], nodeY[i], nodeX[j], nodeY[j]);
                        distanceMatrix[i, j] = dist;
                    }
                    else
                    {
                        distanceMatrix[i, j] = 0;
                    }
                }
            }
        }

        private static long ComputeDistance(long x1, long y1, long x2, long y2)
        {
            double exactDist = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1- y2, 2));
            return Convert.ToInt64(Math.Round(exactDist));
        }

        public static void SaveToFile(String filepath, List<String> lines)
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
