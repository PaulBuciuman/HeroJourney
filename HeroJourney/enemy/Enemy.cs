using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeroJourney
{
    public abstract class Enemy
    {
        private int health;
        private int damage;
        private int bribeAmount;
        private int clumsiness;
        private int goldBounty;

        public int Health { get; set; }
        public int Damage { get => damage; set => damage = value; }
        public int BribeAmount { get => bribeAmount; set => bribeAmount = value; }
        public int Clumsiness { get => clumsiness; set => clumsiness = value; }
        public int GoldBounty { get; set; }


        public void showStats() {

            Console.WriteLine();
            Console.WriteLine("Enemy Health: " + this.Health);
            Console.WriteLine("Enemy Damage: " + this.Damage);
            Console.WriteLine("Enemy Bribe Amount: " + this.BribeAmount);
            Console.WriteLine("Enemy Clumsiness: " + this.Clumsiness);
            Console.WriteLine("Enemy Gold Bounty: "+this.GoldBounty);
            Console.WriteLine();

        }
        public abstract String showEnemyType();
    }
}
