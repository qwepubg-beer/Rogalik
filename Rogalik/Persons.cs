using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public int Damage {  get; set; }
            public int Protection { get; set; }
            public Hero(int MaxHP,int HP,string name,int Damage, int Protection): base(MaxHP, HP, name)
            {
                this.Damage = Damage;
                this.Protection = Protection;  
            }
        }
        public class Enemy : Person
        {
            public int Damage { get;}
            public int Protection { get;}
            public Enemy(int MaxHP, int HP, string name, int Damage, int Protection) : base(MaxHP, HP, name)
            {
                this.Damage = Damage;
                this.Protection = Protection;
            }
        }
    }
}
