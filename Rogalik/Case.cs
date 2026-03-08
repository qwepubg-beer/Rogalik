using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rogalik.Battle;
using static Rogalik.Weapons;
using static Rogalik.Rand;
namespace Rogalik
{
    internal class Case
    {
        public static string Spin()
        {
            if (GetChance(0.50))
            {
                return Regeneration.Value();
            }
            else
            {
                return Items[GetValue(Items.Count - 1)].Value();
            }   
        }
    }
}
