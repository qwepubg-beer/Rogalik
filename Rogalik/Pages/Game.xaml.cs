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
using static Rogalik.Battle;

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
            StartGame();
            UpdateLog("Игра начинается!");
            UpdateE();
            DataContext = MainStaticClass.hero;
        }

        public void UpdateLog(string a)
        {
            MainStaticClass.logs.Add(a);
            Log.ItemsSource = null;
            Log.ItemsSource = MainStaticClass.logs;

            // Автопрокрутка
            if (Log.Items.Count > 0)
            {
                Log.ScrollIntoView(Log.Items[Log.Items.Count - 1]);
            }
        }

        public void UpdateE()
        {
            EnemyListBox.ItemsSource = null;
            EnemyListBox.ItemsSource = MainStaticClass.enemies;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainStaticClass.hero.HP <= 0)
            {
                MessageBox.Show("Герой мертв! Начните новую игру.");
                return;
            }

            if (MainStaticClass.enemies.Count > 0)
            {
                // Герой атакует
                Heroattack();

                // Обновляем список врагов после атаки
                UpdateE();

                // Если враги еще есть, они атакуют
                if (MainStaticClass.enemies.Count > 0)
                {
                    Enemyattack();

                    // Проверяем, не умер ли герой
                    if (MainStaticClass.hero.HP <= 0)
                    {
                        UpdateLog("Герой погиб в бою!");
                        MessageBox.Show("Вы погибли!");
                    }
                }
            }

            // Обновляем интерфейс
            DataContext = null;
            DataContext = MainStaticClass.hero;
            UpdateLog(""); // Пустой вызов для обновления списка логов
            UpdateE();
        }
    }
}
