using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Rogalik
{
    internal class Weapons
    {
        static public double LevelUp = 0.1;
        public class Weapon 
        {
            public string Name { get; }
            public double Damage { get; set; }
            public bool Splash { get; set; }
            public int Level
            {
                get { return Level; }
                set 
                { 
                    Level +=1; Damage = Damage*(1+(LevelUp*Level));
                }
            }
            public Weapon(string name, int damage, bool splash, int level)
            {
                Name = name;
                Damage = damage;
                Splash = splash;
                Level = level;
            }
        }
        public class Armor
        {
            public string Name { get; }
            public double Protection { get; set; }
            public int Level
            {
                get { return Level; }
                set
                {
                    Level += 1; Protection = Protection * (1 + (LevelUp * Level));
                }
            }
            public Armor(string name, double prot, int level)
            {
                Name = name;
                Protection = prot;
                Level = level;
            }
        }

    }
} 
