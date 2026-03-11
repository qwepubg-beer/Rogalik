using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rogalik.Persons;
using static Rogalik.Weapons;

namespace Rogalik
{
    public static class MainStaticClass
    {
        static public Hero hero { get; set; } = Perminov;
        static public List<Enemy> enemies { get; set; }= new List<Enemy>();
        static public List<Enemy> boses { get; set; } = new List<Enemy>();
        static public Enemy boss { get; set; }
        static public int raund { get; set; } = 0;
        public static List<Item> items { get; set; }=new List<Item>();
        public static ObservableCollection<string> logs = new ObservableCollection<string>();

    }
       
}




    