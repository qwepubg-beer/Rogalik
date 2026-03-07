using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogalik
{
    internal class Rand
    {
        public static bool GetChance(double x)
        {
            Random R = new Random();
            bool result = (R.Next(0, 100) <= x * 100) ? true : false;
            return result;
        }
    }
}
