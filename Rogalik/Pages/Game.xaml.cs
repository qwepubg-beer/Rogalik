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
            DataContext = MainStaticClass.hero;
            StartGame();
            UpdateLog("Игра начинается!");
        }

        public void RefreshUI()
        {
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
        }

        private void Button_Attack_Click(object sender, RoutedEventArgs e)
        {
            if (MainStaticClass.hero.HP <= 0)
            {
                MessageBoxResult result = MessageBox.Show(       
                        $"Вы хотите начать заново?",
                        "Сообщение",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                    mainWindow.MainFrame.Navigate(new Game());
                }
                else
                {
                    Application.Current.Shutdown();
                }

                MessageBox.Show("Герой мертв! Начните новую игру.");
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
            UpdateLog("🛡️ Герой готовится к защите!");

            if (MainStaticClass.enemies.Count > 0)
            {
                Enemyattack();
                MainStaticClass.hero.Defending = false;
            }

            RefreshUI();
        }

        private void Button_Case_Click(object sender, RoutedEventArgs e)
        {
            if (MainStaticClass.hero.HP <= 0) return;

            string caseResult = Case.Spin();
            UpdateLog(caseResult);
            RefreshUI();
        }

        public void UpdateLog(string message)
        {
            MainStaticClass.logs.Add(message);
        }
    }
}
