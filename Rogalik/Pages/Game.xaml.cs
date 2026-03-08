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

        List<string> logs = new List<string>();
        public void Update(string a)
        {
            logs.Add(a);
            Log.ItemsSource = logs;
        }
        public Game()
        {
            InitializeComponent();
            MessageBox.Show("Игра начинается!");
            Update("Игра начинается!");
        }
    }
}
