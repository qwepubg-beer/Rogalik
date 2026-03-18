using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rogalik.Weapons;
using static Rogalik.Rand;
using System.Windows.Navigation;

namespace Rogalik
{
    public class Persons
    {

        public static Hero Perminov = new Hero(160, 160, "Перминов", mgu, tsirt, "../Images/perminov.jpg");
        public static Hero Valera = new Hero(240, 240, "Валера", dipseek, paket, "../Images/goblin.jpg");
        public static Hero Veselov = new Hero(150, 150, "Веселов", ball, sport, "../Images/veeselov.jpg");
        public static Hero WrWhite = new Hero(200, 110, "Белый", battle, parik, "../Images/white.jpg");
        public static Hero Prime = new Hero(250, 250, "Перминов Прайм", fstone, sport, "../Images/primeper.jpg");
        public static Hero Lev = new Hero(200, 200, "Лев Денисович", samokat, paket, "../Images/1с3.jpg");
        public static Goblin goblin = new Goblin(30, 30, "Гоблин", 12, 3, "../Images/goblin.jpg");
        public static Skelet skelet = new Skelet(40, 40, "Скелет", 10, 5, "../Images/skelet.png");
        public static Wizard wizard = new Wizard(25, 25, "Маг", 15, 2, "../Images/wizard.png");
        public static List<Enemy> enemies = new List<Enemy> { goblin, skelet, wizard };
        public static List<Hero> heroes = new List<Hero> { Perminov,Veselov, WrWhite,Prime,Valera,Lev };
        public static List<Enemy> boses = new List<Enemy> {
        new Goblin(60, 60, "ВВГ", 18, 4, "../Images/gvv.jpg"),
        new Skelet(60, 60, "Набиев --", 18, 3, "../Images/nabiev.jpg"),
        new Wizard(41, 41, "Архимаг#", 22, 6, "../Images/gordov.jpg"),
        new Skelet(100, 100, "Ковалевский", 12, 7, "../Images/koval.jpg")};

        public class Person
        {
            public int MaxHP { get; set; }
            public double HP { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }

            public Person(int maxHP, int hP, string name, string image)
            {
                MaxHP = maxHP;
                HP = hP;
                Name = name;
                Image = image;
            }
        }

        public class Hero : Person
        {
            public Weapon Damage { get; set; }
            public Armor Protection { get; set; }
            public bool Defending { get; set; } = false;
            public List<Item> Items { get; set; } = new List<Item>();

            public Hero(int MaxHP, int HP, string name, Weapon Wep, Armor Protection, string image)
                : base(MaxHP, HP, name, image)
            {
                Damage = Wep;
                this.Protection = Protection;
                ChangeItems();
            }
            public void ChangeItems()
            {
                Items.Clear();
                Items.Add(Damage);
                Items.Add(Protection);
            }

            public double ReturnDamage(double enemyProtection)
            {
                double damage = Damage.Damage - enemyProtection;
                if(Defending)
                {
                    bool a = GetChance(0.50);
                    damage = a ? damage / 2 : 0;
                    return damage;
                }
                else
                {
                    return damage > 0 ? damage : 1;
                }

                
            }

            public double TakeDamage(double damage)
            {
                if (Defending)
                {
                    damage /= 2;
                    Defending = false;
                }
                double actualDamage = damage - Protection.Protection;
                actualDamage = actualDamage > 0 ? actualDamage : 1; 
                HP -= actualDamage;
                if (HP < 0) HP = 0;
                return actualDamage;
            }
        }

        public class Enemy : Person 
        {
            public double Damage { get; set; }
            public double Protection { get; set; }

            public Enemy(int MaxHP, int HP, string name, double Damage, double Protection, string image)
                : base(MaxHP, HP, name, image)
            {
                this.Damage = Damage;
                this.Protection = Protection;
            }

            public virtual double ReturnDamage(double heroProtection, bool defending)
            {
                double damage = defending? Damage/2 - heroProtection : Damage - heroProtection;
                return damage > 0 ? damage : 1;
            }
        }

        public class Goblin : Enemy
        {
            public double Krit { get; set; } = 0.2;

            public Goblin(int MaxHP, int HP, string name, double Damage, double Protection, string image)
                : base(MaxHP, HP, name, Damage, Protection, image)
            {
            }

            public override double ReturnDamage(double heroProtection, bool defending)
            {
                double damage = defending ? Damage / 2 - heroProtection : Damage - heroProtection;
                damage = damage > 0 ? damage : 1;

                if (Rand.GetChance(Krit))
                {
                    damage *= 2;
                }
                return damage;
            }
        }

        public class Skelet : Enemy
        {

            public Skelet(int MaxHP, int HP, string name, double Damage, double Protection, string image)
                : base(MaxHP, HP, name, Damage, Protection, image)
            {

            }

            public override double ReturnDamage(double heroProtection, bool defending)
            {
                double damage = defending ? Damage / 2 : Damage;
                return damage > 0 ? damage : 1;
            }
        }

        public class Wizard : Enemy
        {
            public double Skip { get; set; } = 0.15;

            public Wizard(int MaxHP, int HP, string name, double Damage, double Protection, string image)
                : base(MaxHP, HP, name, Damage, Protection, image)
            {
            }

            public override double ReturnDamage(double heroProtection, bool defending)
            {
                double damage = defending ? Damage / 2 - heroProtection : Damage - heroProtection;
                damage = damage > 0 ? damage : 1;

                if (Rand.GetChance(Skip))
                {
                    damage *= 2;
                }
                return damage;
            }
        }
    }
}
