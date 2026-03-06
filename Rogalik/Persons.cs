using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rogalik.Weapons;

namespace Rogalik
{
    internal class Persons
    {
        public class Person 
        {   
            int MaxHP { get; set; }
            int HP {  get; set; }
            public string name { get; set; }
            public Person(int maxHP, int hP, string name)
            {
                MaxHP = maxHP;
                HP = hP;
                this.name = name;
            }
        }
        public class Hero : Person 
        {
            public Weapon Damage {  get; set; }
            public Armor Protection { get; set; }
            public Hero(int MaxHP,int HP,string name, Weapon Wep, Armor Protection): base(MaxHP, HP, name)
            {
                this.Damage = Wep;
                this.Protection = Protection;  
            }
        }
        public class Enemy : Person
        {
            public double Damage { get;}
            public double Protection { get;}
            public Enemy(int MaxHP, int HP, string name, double Damage, double Protection) : base(MaxHP, HP, name)
            {
                this.Damage = Damage;
                this.Protection = Protection;
            }
        }
    }
}
