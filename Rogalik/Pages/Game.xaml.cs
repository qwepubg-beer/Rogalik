using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Rogalik.Persons;
using static Rogalik.Weapons;
namespace Rogalik.Pages
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        public Game()
        {
            InitializeComponent();
            DataContext = MainStaticClass.hero;
            StartGame();
            UpdateLog("Игра начинается!");
            RefreshUI();
        }

        public void RefreshUI()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action(() => RefreshUI()));
                return;
            }
            DataContext = null;
            DataContext = MainStaticClass.hero;
            EnemyListBox.ItemsSource = null;
            EnemyListBox.ItemsSource = MainStaticClass.enemies;
            Log.ItemsSource = null;
            Log.ItemsSource = MainStaticClass.logs;
            if (InventoryListBox != null)
            {
                InventoryListBox.ItemsSource = null;
                InventoryListBox.ItemsSource = MainStaticClass.hero.Items;
            }
            if (Log.Items.Count > 0)
            {
                Log.ScrollIntoView(Log.Items[Log.Items.Count - 1]);
            }
            ArmorTextBlock.Text= $"{Math.Round(MainStaticClass.hero.Protection.Protection,2)}";
            WeaponTextBlock.Text = $"{Math.Round(MainStaticClass.hero.Damage.Damage,2)}";
            RoundTextBlock.Text= $"{MainStaticClass.raund}";
        }

        private void Button_Attack_Click(object sender, RoutedEventArgs e)
        {
            if (MainStaticClass.hero.HP <= 0)
            {
                MessageBox.Show("Герой мертв! Начните новую игру.");
                Application.Current.Shutdown();
                return;
            }

            if (MainStaticClass.enemies.Count > 0)
            {
                Heroattack();
                RefreshUI();
                if (MainStaticClass.enemies.Count == 0 && MainStaticClass.hero.HP > 0)
                {
                    StartGame();
                    RefreshUI();
                }
            }
            else
            {
                StartGame();
                RefreshUI();
            }
        }

        private void Button_Defend_Click(object sender, RoutedEventArgs e)
        {
            if (MainStaticClass.hero.HP <= 0) return;

            MainStaticClass.hero.Defending = true;
            UpdateLog("Герой готовится к защите!");

            if (MainStaticClass.enemies.Count > 0)
            {
                Enemyattack();
                MainStaticClass.hero.Defending = false;
            }
            RefreshUI();
        }
        public void UpdateLog(string message)
        {
            MainStaticClass.logs.Add(message);
        }
        static public void StartGame()
        {
            MainStaticClass.raund += 1;
            MainStaticClass.logs.Add($"Раунд номер {MainStaticClass.raund}");
            MainStaticClass.enemies.Clear();
            battlesystem(MainStaticClass.hero);
        }
        static void battle(Hero p, Enemy e)
        {
            int CountEnemy = Rand.GetValue(3) + 1;
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
                MainStaticClass.logs.Add($"Вы нанесли {damage:F1} урона {target.Name}");
            }
            else
            {
                // Атака по всем врагам
                foreach (Enemy e in MainStaticClass.enemies.ToList())
                {
                    double damage = MainStaticClass.hero.ReturnDamage(e.Protection);
                    e.HP -= damage;
                }
                MainStaticClass.logs.Add($"Вы нанесли урон по области всем врагам!");
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
                    MainStaticClass.logs.Add($"{enemy.Name} повержен!");
                }
            }
        }
        static public void UpdateEnemy()
        {
            if (MainStaticClass.enemies.Count == 0 && MainStaticClass.hero.HP > 0)
            {
                MainStaticClass.logs.Add($"Все враги побеждены!");
                StartGame();
            }
        }
        static public void Enemyattack()
        {
            if (MainStaticClass.enemies.Count == 0) return;
            MainStaticClass.logs.Add($"Враги атакуют в ответ!");
            foreach (Enemy e in MainStaticClass.enemies.ToList())
            {
                double damage = e.ReturnDamage(MainStaticClass.hero.Protection.Protection);
                MainStaticClass.hero.HP -= damage;
                MainStaticClass.logs.Add($"💥 {e.Name} нанес {damage:F1} урона");
                if (MainStaticClass.hero.HP <= 0)
                {
                    MainStaticClass.hero.HP = 0;
                    MainStaticClass.logs.Add($"Герой погиб в бою!");
                    MessageBox.Show("Game Over!");
                    return;
                }
            }
        }
        static void CaseOrBattle(Hero p, Enemy e)
        {
            if (Rand.GetChance(0.50))
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
                Application.Current.Shutdown();
                return;
            }

            // Очищаем врагов перед новой битвой
            MainStaticClass.enemies.Clear();

            if (MainStaticClass.raund == 10)
            {
                MainStaticClass.logs.Add($"БИТВА С БОССОМ!");
                battle(p, boses[Rand.GetValue(boses.Count)]);
            }
            else
            {
                CaseOrBattle(p, enemies[Rand.GetValue(enemies.Count)]);
            }
        }
        public static string Spin()
        {
            if (Rand.GetChance(0.50))
            {
                return HealingPotion();
            }
            else
            {
                if(Rand.GetChance(0.50))
                {
                    return Upgrade();
                }
                else
                {
                    return GetRandomItem();
                }
               
            }
        }

        private static string HealingPotion()
        {
            double oldHP = MainStaticClass.hero.MaxHP;
            MainStaticClass.hero.HP = MainStaticClass.hero.MaxHP;
            string message = $"Зелье исцеления! Восстановлено HP";
            MainStaticClass.logs.Add(message);
            return message;
        }

        private static string GetRandomItem()
        {
            int itemIndex = Rand.GetValue(Items.Count);
            Item foundItem = Items[itemIndex];
            Item newItem = CreateItemCopy(foundItem);
            string message = "";
            if (newItem is Weapon newWeapon)
            {
                message = WeaponDrop(newWeapon);
            }
            else if (newItem is Armor newArmor)
            {
                message = ArmorDrop(newArmor);
            }
            else
            {
                MainStaticClass.hero.Items.Add(newItem);
                message = $"Вы нашли: {newItem.Name}";
            }
            MainStaticClass.logs.Add(message);
            return message;
        }
        private static string UpgradeWeaponDrop(UpgradeWeapon newWeapon)
        {
            MainStaticClass.hero.Damage.LevelUp();
            UpdateUI();
            return newWeapon.Value();
        }
        private static string UpgradeArmorDrop(UpgradeArmor newWeapon)
        {
            MainStaticClass.hero.Protection.LevelUp();
            UpdateUI();
            return newWeapon.Value();
        }
        private static string Upgrade()
        {
            MainStaticClass.hero.Protection.LevelUp();
            MainStaticClass.hero.Damage.LevelUp();
            UpdateUI();
            return $"Ваши показатели улучшены!";
        }
        private static string WeaponDrop(Weapon newWeapon)
        {
            var currentWeapon = MainStaticClass.hero.Damage;
                if (currentWeapon != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"Найдено новое оружие: {newWeapon.Name} (Урон: {newWeapon.Damage})\n\n" +
                        $"Текущее оружие: {currentWeapon.Name} (Урон: {currentWeapon.Damage})\n\n" +
                        $"Заменить?",
                        "Новое оружие",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        MainStaticClass.hero.Damage = newWeapon;
                        MainStaticClass.logs.Add($"Вы экипировали {newWeapon.Name} (Урон: {newWeapon.Damage})");
                    }
                    else
                    {
                        MainStaticClass.hero.Items.Add(newWeapon);
                        MainStaticClass.logs.Add($"Вы выбросили  {newWeapon.Name}");
                    }
                    UpdateUI();
                }
                return $"Новое оружие: {newWeapon.Name} (Урон: {newWeapon.Damage}";

        }
        private static void UpdateUI()
        {
            var mainWindow = Application.Current.MainWindow;
            var frame = mainWindow?.Content as Frame;
            if (frame?.Content is Game gamePage)
            {
                gamePage.Dispatcher.BeginInvoke(new Action(() =>
                {
                    gamePage.RefreshUI();
                }));
            }
        }
        private static string ArmorDrop(Armor newArmor)
        {
            if (MainStaticClass.hero.Protection != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Найдена новая броня: {newArmor.Name} (Защита: {newArmor.Protection})\n\n" +
                    $"Текущая броня: {MainStaticClass.hero.Protection.Name} (Защита: {MainStaticClass.hero.Protection.Protection})\n\n" +
                    $"Заменить?",
                    "Новая броня",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MainStaticClass.hero.Protection = newArmor;
                    return $"Вы экипировали {newArmor.Name} (Защита: {newArmor.Protection})";
                }
                else
                {
                    return $"Вы выбросили {newArmor.Name}";
                }
            }
            else
            {
                MainStaticClass.hero.Protection = newArmor;
                return $"Вы получили {newArmor.Name} (Защита: {newArmor.Protection})";
            }
        }

        private static Item CreateItemCopy(Item original)
        {
            if (original is Weapon weapon)
            {
                return new Weapon(weapon.Name, weapon.Damage, weapon.Splash);
            }
            else if (original is Armor armor)
            {
                return new Armor(armor.Name, armor.Protection);
            }
            else
            {
                return new Item(original.Name);
            }
        }
    }
}
