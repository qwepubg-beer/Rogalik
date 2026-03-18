using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Rogalik.Weapons;
using static System.Net.Mime.MediaTypeNames;

namespace Rogalik
{
    public class Weapons
    {
        public static Item Regeneration = new Item("Зелье регенерации");
        public static Weapon calculater = new Weapon("Калькулятор 1С", 15, false);
        public static Armor paket = new Armor("Пакет 1С", 8);
        public static Armor tsirt = new Armor("Жёлтая рубашка", 8);
        public static Armor parik = new Armor("Парик", 9);
        public static Armor mask = new Armor("Маска pididi", 10);
        public static Weapon samokat = new Weapon("Самокат", 12,true);
        public static Armor sport = new Armor("Спортивный костюм", 12);
        public static Weapon mgu = new Weapon("Диплом мгу", 15, false);
        public static Weapon battle = new Weapon("Бутылка водки", 20, false);
        public static Weapon fstone = new Weapon("Филосовский камень", 20, true);
        public static Weapon dipseek = new Weapon("Дипсик набиева", 10, true);
        public static Weapon ball = new Weapon("Баскетбольный мяч", 16, false);
        public static Item up = new Item("Улучшение вооружения");
        public static List<Item> Items = new List<Item> {mgu, calculater, paket, dipseek, ball, tsirt,mask,battle,sport,parik,fstone, samokat };
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
                //Damage++;
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
                    return $"оружие: {Name}, Урон по области: {Damage}, Уровень: {Level}";
                }
                else
                {
                    return $"оружие: {Name}, Урон: {Damage}, Уровень: {Level}";
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
                //Protection++;
                Protection = Protection * (1 + (Up * Level));
            }

            public Armor(string name, double prot) : base(name)
            {
                Protection = prot;
                Level = 1;
            }

            public override string Value()
            {
                return $"броня: {Name}, Защита: {Protection}, Уровень: {Level}";
            }
        }
        public class UpgradeWeapon : Item
        {
            public char description { get; set; }

            public UpgradeWeapon(string name, char description) : base(name)
            {
                this.description = description;
            }

            public override string Value()
            {
                return $"Вам выпало {Name}{description}оружия";
            }
        }
        public class UpgradeArmor : Item
        {
            public string description {  get; set; }
            public UpgradeArmor(string name, string description) : base(name)
            {
                this.description = description;
            }

            public override string Value()
            {
                return $"Вам выпало {Name} {description} ";
            }
        }
    }
} 
