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
            MainStaticClass.logs.Add($"=== Раунд номер {MainStaticClass.raund} ===");

            // Очищаем список врагов перед новым раундом
            MainStaticClass.enemies.Clear();

            battlesystem(MainStaticClass.hero);
        }
        static void battle(Hero p, Enemy e)
        {
            int CountEnemy = GetValue(3);
            MainStaticClass.logs.Add($"Появилось {CountEnemy} врагов типа {e.name}");

            for (int i = 0; i < CountEnemy; i++)
            {
                Enemy newEnemy = new Enemy
                {
                    name = e.name,
                    HP = e.MaxHP,
                    MaxHP = e.MaxHP,
                    Protection = e.Protection,
                    Damage = e.Damage,
                    image= e.image
                };
                MainStaticClass.enemies.Add(newEnemy);
            }
        }
        static public void Heroattack()
        {
            if (!MainStaticClass.hero.Damage.Splash)
            {
                if (MainStaticClass.enemies.Count > 0)
                {
                    MainStaticClass.enemies[0].HP -= MainStaticClass.hero.ReturnDamage(MainStaticClass.enemies[0].Protection);
                    MainStaticClass.logs.Add($"Вы нанесли урон врагу. У врага осталось {MainStaticClass.enemies[0].HP} HP");
                }
            }
            else
            {
                foreach (Enemy e in MainStaticClass.enemies.ToList()) // Используем ToList() для копии
                {
                    e.HP -= MainStaticClass.hero.ReturnDamage(MainStaticClass.enemies[0].Protection);
                }
                MainStaticClass.logs.Add($"Вы нанесли урон всем врагам!");
            }

            // Вызываем UpdateEnemy только один раз
            UpdateEnemy();
        }
        static public void UpdateEnemy()
        {
            if (MainStaticClass.enemies.Count > 0)
            {
                // Создаем копию списка для итерации
                var enemiesToRemove = MainStaticClass.enemies.Where(e => e.HP <= 0).ToList();

                foreach (var enemy in enemiesToRemove)
                {
                    MainStaticClass.enemies.Remove(enemy);
                    MainStaticClass.logs.Add($"Враг повержен!");
                }

                // Если врагов не осталось, начинаем новый раунд
                if (MainStaticClass.enemies.Count == 0)
                {
                    StartGame();
                }
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
                string caseResult = Spin(); // Ваш метод Spin()
                MainStaticClass.logs.Add($"Кейс: {caseResult}");
                MessageBox.Show(caseResult);
                StartGame(); // После кейса сразу новый раунд
            }
            else
            {
                battle(p, e);
            }
        }
        static void battlesystem(Hero p)
        {
            if (p.HP <= 0)
            {
                MainStaticClass.logs.Add("Герой погиб! Игра окончена.");
                MessageBox.Show("Game Over!");
                return;
            }

            // Очищаем врагов перед новой битвой
            MainStaticClass.enemies.Clear();

            if (MainStaticClass.raund == 10)
            {
                MainStaticClass.logs.Add($"БИТВА С БОССОМ!");
                battle(p, boses[GetValue(boses.Count)]);
            }
            else
            {
                CaseOrBattle(p, enemies[GetValue(enemies.Count)]);
            }
        }
    }
        }
    }
}
