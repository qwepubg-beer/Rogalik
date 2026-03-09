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
        public void UpdateLog(string a)
        {
            MainStaticClass.logs.Add(a);
            Log.ItemsSource = MainStaticClass.logs;
        }
        public void UpdateE()
        {
            EnemyListBox.ItemsSource = MainStaticClass.enemies;
        }
        public Game()
        {
            DataContext = MainStaticClass.hero;
            InitializeComponent();
            StartGame();
            UpdateLog("Игра начинается!");
            EnemyListBox.ItemsSource = MainStaticClass.enemies;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainStaticClass.enemies.Count > 0)
            {
                Heroattack(); UpdateE();
            }
            else 
            {
                StartGame();
            }
            Enemyattack();
            DataContext=MainStaticClass.hero;
        }
    }
}
