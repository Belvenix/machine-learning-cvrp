using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningCVRP.Utility
{
    class Shuffler
    {
        public static List<int> GenereteShuffledOrder(int size, Random rnd)
        {
            List<int> optOrder = Enumerable.Range(0, size).ToList();
            for (var i = optOrder.Count; i > 0; i--)
                Swap(optOrder, i - 1, rnd.Next(0, i));
            for (var i = 0; i < optOrder.Count; i++)
                Swap(optOrder, i, rnd.Next(i, optOrder.Count));
            return optOrder;
        }

        private static void Swap(List<int> list, int i, int j)
        {
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
