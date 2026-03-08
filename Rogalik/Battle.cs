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
using static Rogalik.Case;
using System.Windows;

namespace Rogalik
{
    internal class Battle
    {
        static public void StartGame()
        {
            battlesystem(MainStaticClass.hero, MainStaticClass.enemies, MainStaticClass.boses);
        }
        static void battle(Hero p, Enemy e)
        {
            
        }
        static void CaseOrBattle(Hero p, Enemy e)
        {
            if (GetChance(0.50))
            {
                MessageBox.Show(Spin());
            }
            else
            {
                battle(p, e);
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
                foreach (Enemy a in bosses)
                {
                    a.HP = a.MaxHP;
                }
                round += 1;
                Console.WriteLine($"бой номер {round}");
                if (round == 10) { MessageBox.Show($"Битва с босом"); battle(p, bosses[GetValue(4)]); }
                else { CaseOrBattle(p, enemies[GetValue(3)]); }
            }
            MessageBox.Show("Вы проиграли");
        }
    }
}
