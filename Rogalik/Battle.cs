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
            int CountEnemy = GetValue(3) + 1;
            MainStaticClass.logs.Add($"Появилось {CountEnemy} врагов типа {e.Name}");

            for (int i = 0; i < CountEnemy; i++)
            {
                // Создаем копию через конструктор копирования
                Enemy newEnemy = (Enemy)Activator.CreateInstance(e.GetType(),
                    e.MaxHP, e.MaxHP, e.Name, e.Damage, e.Protection, e.Image);

                MainStaticClass.enemies.Add(newEnemy);
            }
        }
        static public void Heroattack()
        {
            if (MainStaticClass.enemies.Count == 0) return;

            // Запоминаем, был ли сплеш урон
            bool wasSplash = MainStaticClass.hero.Damage.Splash;

            // Герой атакует
            if (!wasSplash)
            {
                // Атака по первому врагу
                Enemy target = MainStaticClass.enemies[0];
                double damage = MainStaticClass.hero.ReturnDamage(target.Protection);
                target.HP -= damage;
                MainStaticClass.logs.Add($"⚔️ Вы нанесли {damage:F1} урона {target.Name}");
            }
            else
            {
                // Атака по всем врагам
                foreach (Enemy e in MainStaticClass.enemies.ToList())
                {
                    double damage = MainStaticClass.hero.ReturnDamage(e.Protection);
                    e.HP -= damage;
                }
                MainStaticClass.logs.Add($"⚔️ Вы нанесли урон по области всем врагам!");
            }
            CheckDeadEnemies();
            if (MainStaticClass.enemies.Count > 0)
            {
                Enemyattack();
            }
        }

        // Новый метод для проверки мертвых врагов
        static public void CheckDeadEnemies()
        {
            if (MainStaticClass.enemies.Count > 0)
            {
                var deadEnemies = MainStaticClass.enemies.Where(e => e.HP <= 0).ToList();

                foreach (var enemy in deadEnemies)
                {
                    MainStaticClass.enemies.Remove(enemy);
                    MainStaticClass.logs.Add($"💀 {enemy.Name} повержен!");
                }
            }
        }
        static public void UpdateEnemy()
        {
            // Этот метод теперь только проверяет, не пора ли начать новый раунд
            if (MainStaticClass.enemies.Count == 0 && MainStaticClass.hero.HP > 0)
            {
                MainStaticClass.logs.Add($"🎉 Все враги побеждены!");
                StartGame();
            }
        }
        static public void Enemyattack()
        {
            if (MainStaticClass.enemies.Count == 0) return;

            MainStaticClass.logs.Add($"👹 Враги атакуют в ответ!");

            foreach (Enemy e in MainStaticClass.enemies.ToList()) // ToList() для безопасности
            {
                double damage = e.ReturnDamage(MainStaticClass.hero.Protection.Protection);
                MainStaticClass.hero.HP -= damage;
                MainStaticClass.logs.Add($"💥 {e.Name} нанес {damage:F1} урона");

                // Проверяем, не умер ли герой
                if (MainStaticClass.hero.HP <= 0)
                {
                    MainStaticClass.hero.HP = 0;
                    MainStaticClass.logs.Add($"💔 Герой погиб в бою!");
                    MessageBox.Show("Game Over!");
                    return;
                }
            }
        }
        static void CaseOrBattle(Hero p, Enemy e)
        {
            if (GetChance(0.50))
            {
                string caseResult = Spin(); 
                MainStaticClass.logs.Add($"Кейс: {caseResult}");
                MessageBox.Show(caseResult);
                StartGame();
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
