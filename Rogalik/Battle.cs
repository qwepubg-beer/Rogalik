using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Rogalik.Persons;
using static Rogalik.Rand;
using static Rogalik.Weapons;

namespace Rogalik
{
    internal class Battle
    {
        static public void StartGame()
        {
            
        }
        static void battle(Hero p, Enemy e)
        {
            
        }
        static void CaseOrBattle(Hero p, Enemy e)
        {
            //Thread.Sleep(1000);
            if (GetChance(0.50))
            {
                
            }
            else
            {

            }
        }
        static void battlesystem(Hero p, List<Enemy> enemies, List<Enemy> bosses)
        {
            int round = 0;
            while (p.HP > 0)
            {
                foreach (Enemy a in enemies)
                {
                    a.HP = a.MaxHP;
                }
                foreach (Enemy a in enemies)
                {
                    a.HP = a.MaxHP;
                }
                round += 1;
                Console.WriteLine($"бой номер {round}");
                if (round == 10) { Console.WriteLine($"Битва с босом"); battle(p, bosses[GetValue(4)]); }
                else { CaseOrBattle(p, enemies[GetValue(3)]); }
            }
            Console.WriteLine("Вы проиграли");

        }
    }
}
