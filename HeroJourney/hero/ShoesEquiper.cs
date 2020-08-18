using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Transactions;

namespace HeroJourney.hero
{
    class ShoesEquiper:HeroEquiper
    {   
        public ShoesEquiper(Hero hero) : base(hero) { this.Cost = 800; }
        
        public override void equip()
        {
            hero.Equipment.Add("Shoes");
            hero.Gold -= this.Cost;
        }



    }
}
