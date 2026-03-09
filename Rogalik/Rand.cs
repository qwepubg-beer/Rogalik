using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rogalik
{
    internal class Rand
    {
        private static Random _random = new Random();
        private static readonly object _lock = new object();

        public static bool GetChance(double x)
        {
            lock (_lock)
            {
                return _random.NextDouble() < x;
            }
        }

        public static int GetValue(int x)
        {
            if (x <= 0) return 0;
            lock (_lock)
            {
                return _random.Next(x);
            }
        }
    }
}
