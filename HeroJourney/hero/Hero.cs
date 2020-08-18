using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HeroJourney
{
    public abstract class Hero
    {
        private int health;
        private int damage;
        private int gold;
        private string name;
        private ArrayList equipment = new ArrayList();

        public int Health { get => health; set => health = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Gold { get => gold; set => gold = value; }
        public string Name { get => name; set => name = value; }

        public ArrayList Equipment { get => equipment;}


        public void fight(ref Enemy enemy) {
            Console.WriteLine("\nYour hero is fighting the enemy!\n");
            while (enemy.Health > 0 && this.Health > 0) {
                enemy.Health = enemy.Health - this.Damage;
                this.Health = this.Health - enemy.Damage;
            }
            if (enemy.Health <= 0 && this.Health > 0)
            {
                this.Gold += enemy.GoldBounty;
                Console.WriteLine("\nYou defeated the enemy!\n");

            }
            else if (this.Health <= 0) Console.WriteLine("\nYour hero died!\n");

        }
        public void sneak(ref Enemy enemy) {
            Console.WriteLine("\nYou are trying to sneak in!\n");

            Random r = new Random();
            int sneakChance = r.Next(1, 101);
            if (this.Equipment.Contains("Shoes"))
                sneakChance += 10;
            
            if (sneakChance <= enemy.Clumsiness)
            {
                Console.WriteLine("\nYou sneaked in successfully!\n");
            }
            else
            {
                Console.WriteLine("\nThe enemy got you!\n");
                enemy.Damage *= 2;
                enemy.Health *= 2;
                this.fight(ref enemy);
            }
        }
        public void bribe(ref Enemy enemy) {
            Console.WriteLine("\nYou are trying to bribe the enemy!\n");

            if (this.Gold > enemy.BribeAmount){
                this.Gold = this.Gold - enemy.BribeAmount;
                Console.WriteLine("\nBine bulan, ai dat mita!\n");
            }
            else {
                Console.WriteLine("You cannot bribe this enemy! You need to choose another action.");
            }
        
        }
        public void showStats() {
            Console.WriteLine();

            Console.WriteLine(this.Name + "'s Health: " + this.Health);
            Console.WriteLine(this.Name + "'s Damage: " + this.Damage);
            Console.WriteLine(this.Name + "'s Gold: " + this.Gold);
            Console.WriteLine();
        }

        public void showEquipment() { 
            foreach (String e in equipment)
                Console.WriteLine("Your hero is equiped with: "+e);
        }



    }
}
