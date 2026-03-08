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
            int CountEnemy = GetValue(3);
            for (int i = 0;i<CountEnemy;i++)
            {
                MainStaticClass.enemies[i] = e;
            }
        }
        static void Heroattack()
        {
            if (!MainStaticClass.hero.Damage.Splash) 
            {
                MainStaticClass.enemies[0].HP -= MainStaticClass.hero.ReturnDamage(MainStaticClass.hero, MainStaticClass.enemies[0]);
            }
            else
            {
                foreach (Enemy e in MainStaticClass.enemies) 
                {
                    e.HP -= MainStaticClass.hero.ReturnDamage(MainStaticClass.hero, MainStaticClass.enemies[0]);
                }
            }
        }
        static void Enemyattack()
        {
            foreach (Enemy e in MainStaticClass.enemies)
            {
                MainStaticClass.hero.HP -= e.ReturnDamage(MainStaticClass.hero, MainStaticClass.enemies[0]);
            }
            MainStaticClass.hero.HP -= MainStaticClass.hero.ReturnDamage(MainStaticClass.hero, MainStaticClass.enemies[0]);
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
            if (p.HP > 0)
            {
                foreach (Enemy a in enemies)
                {
                    a.HP = a.MaxHP;
                }
                foreach (Enemy a in bosses)
                {
                    a.HP = a.MaxHP;
                }
                if (round == 10) { MessageBox.Show($"Битва с босом"); battle(p, bosses[GetValue(4)]); }
                else { CaseOrBattle(p, enemies[GetValue(3)]); }
            }
            MessageBox.Show("Вы проиграли");
        }
    }
}
