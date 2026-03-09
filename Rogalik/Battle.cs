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
            MainStaticClass.raund += 1;
            MainStaticClass.logs.Add($"Раунд номер {MainStaticClass.raund}");

            battlesystem(MainStaticClass.hero);
        }
        static void battle(Hero p, Enemy e)
        {
            int CountEnemy = GetValue(3);
            for (int i = 0; i < CountEnemy; i++)
            {
                MainStaticClass.enemies.Add(e);
            }
        }
        static public void Heroattack()
        {
            if (!MainStaticClass.hero.Damage.Splash) 
            {
                UpdateEnemy();
                MainStaticClass.enemies[0].HP -= MainStaticClass.hero.ReturnDamage(MainStaticClass.enemies[0].Protection);
            }
            else
            {
                foreach (Enemy e in MainStaticClass.enemies) 
                {
                    UpdateEnemy();
                    e.HP -= MainStaticClass.hero.ReturnDamage(MainStaticClass.enemies[0].Protection);
                }
            }
        }
        static public void UpdateEnemy()
        {
            foreach (Enemy e in MainStaticClass.enemies)
            {
                if (e.HP <= 0) { MainStaticClass.enemies.Remove(e); }
            }
        }
        static public void Enemyattack()
        {
            foreach (Enemy e in MainStaticClass.enemies)
            {
                MainStaticClass.hero.HP -= e.ReturnDamage(MainStaticClass.hero.Protection.Protection);
            }
        }
        static void CaseOrBattle(Hero p, Enemy e)
        {
            if (GetChance(0.50))
            {
                MessageBox.Show(Spin());
                StartGame();
            }
            else
            {
                battle(p, e);
            }
        }
        static void battlesystem(Hero p)
        {
            if (p.HP > 0)
            {
                foreach (Enemy a in MainStaticClass.enemies)
                {
                    a.HP = a.MaxHP;
                }
                foreach (Enemy a in MainStaticClass.boses)
                {
                    a.HP = a.MaxHP;
                }
                if (MainStaticClass.raund == 10) 
                { 
                    MessageBox.Show($"Битва с босом"); 
                    battle(p, boses[GetValue(4)]); 
                }
                else 
                { 
                    CaseOrBattle(p, enemies[GetValue(3)]);
                }
            }
        }
    }
}
