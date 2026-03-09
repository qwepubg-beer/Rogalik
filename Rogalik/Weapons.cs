using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Rogalik
{
    public class Weapons
    {
        public static Item Regeneration = new Item("Зелье регенерации");
        public static Weapon Axe = new Weapon("Топор", 10, false);
        public static Armor iron = new Armor("Железная броня", 8);
        public static Weapon Sword = new Weapon("Меч", 9, false);
        public static List<Item> Items = new List<Item> { Axe, Sword, Regeneration };

        static public double Up = 0.1;

        public class Item
        {
            public string Name { get; set; }

            public Item(string name)
            {
                Name = name;
            }

            public virtual string Value()
            {
                return $"Вам выпал предмет: {Name}";
            }
        }

        public class Weapon : Item
        {
            public double Damage { get; set; }
            public bool Splash { get; set; }
            public int Level { get; set; }

            public void LevelUp()
            {
                Level++;
                Damage = Damage * (1 + (Up * Level));
            }

            public Weapon(string name, double damage, bool splash) : base(name)
            {
                Damage = damage;
                Splash = splash;
                Level = 1;
            }

            public override string Value()
            {
                if (Splash)
                {
                    return $"Вам выпал предмет: {Name}, Урон по области: {Damage}, Уровень: {Level}";
                }
                else
                {
                    return $"Вам выпал предмет: {Name}, Урон: {Damage}, Уровень: {Level}";
                }
            }
        }

        public class Armor : Item
        {
            public double Protection { get; set; }
            public int Level { get; set; }

            public void LevelUp()
            {
                Level++;
                Protection = Protection * (1 + (Up * Level));
            }

            public Armor(string name, double prot) : base(name)
            {
                Protection = prot;
                Level = 1;
            }

            public override string Value()
            {
                return $"Вам выпал предмет: {Name}, Защита: {Protection}, Уровень: {Level}";
            }
        }
    }
} 
