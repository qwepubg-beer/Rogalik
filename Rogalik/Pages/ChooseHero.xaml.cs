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

namespace Rogalik.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChooseHero.xaml
    /// </summary>
    public partial class ChooseHero : Page
    {
        public ChooseHero()
        {
            InitializeComponent();
            HeroListBox.ItemsSource = heroes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (HeroListBox.SelectedItem != null) 
            {
                HeroListBox.SelectedItem=(Hero)MainStaticClass.hero;
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MainFrame.Navigate(new StartGamePage());
            }
        }
    }
}
