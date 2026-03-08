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
        public static Hero Perminov = new Hero(30, 30, "Гоблин", Axe, iron, "");
        public static Goblin goblin = new Goblin(30, 30, "Гоблин", 12, 3,"");
        public static Goblin skelet = new Goblin(40, 40, "Скелет", 10, 5, "");
        public static Goblin wizard = new Goblin(25, 25, "Маг", 15, 2, "");
        public static List<Enemy> enemies = new List<Enemy> {goblin,skelet,wizard};
        public static List<Enemy> boses = new List<Enemy> { goblin, skelet, wizard };
        public class Person 
        {   
            public int MaxHP { get; set; }
            public double HP {  get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public Person(int maxHP, int hP, string name, string image)
            {
                MaxHP = maxHP;
                HP = hP;
                this.name = name;
                this.image = image;
            }
        }
        public class Hero : Person 
        {
            public Weapon Damage {  get; set; }
            public Armor Protection { get; set; }
            public Hero(int MaxHP,int HP,string name, Weapon Wep, Armor Protection, string image) : base(MaxHP, HP, name,image)
            {
                this.Damage = Wep;
                this.Protection = Protection;  
            }
            public double ReturnDamage(Hero hero, Enemy e)
            {
                double at = hero.Damage.Damage * e.Protection;
                return at;
            }
        }
        public class Enemy : Person
        {
            public double Damage { get;}
            public double Protection { get;}
            public Enemy(int MaxHP, int HP, string name, double Damage, double Protection, string image) : base(MaxHP, HP, name, image)
            {
                this.Damage = Damage;
                this.Protection = Protection;
            }
        }
        public class Goblin: Enemy
        {
            public static double krit { get; set; } = 0.2;
            public Goblin(int MaxHP, int HP, string name, double Damage, double Protection, string image) : base (MaxHP, HP, name, Damage, Protection,image) 
            {
            }
            public double ReturnDamage(Hero hero,Goblin g)
            {
                double at = GetChance(krit) ? g.Damage * hero.Protection.Protection * 2 : g.Damage * hero.Protection.Protection;
                return at;
            }
        }
        public class Skelet : Enemy
        {
            public int armor { get; set; } = 0;
            public Skelet(int MaxHP, int HP, string name, double Damage, double Protection, int krit, string image) : base(MaxHP, HP, name, Damage, Protection,image)
            {
                this.armor = krit;
            }
            public double ReturnDamage(Hero hero, Skelet s)
            {
                double at = s.Damage;
                return at;
            }
        }
        public class Wizard : Enemy
        {
            public double skip { get; set; } = 0.15;
            public Wizard(int MaxHP, int HP, string name, double Damage, double Protection, double krit, string image) : base(MaxHP, HP, name, Damage, Protection,image)
            {
                skip = krit;
            }
            public double ReturnDamage(Hero hero, Wizard w)
            {
                double at = GetChance(skip) ? w.Damage * hero.Protection.Protection * 2 : w.Damage * hero.Protection.Protection;
                return at;
            }
        }
    }
}
