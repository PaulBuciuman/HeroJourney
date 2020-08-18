using System;
using System.Collections.Generic;
using System.Text;

namespace HeroJourney.hero
{
    class ArmorEquiper : HeroEquiper
    {

        public override void equip()
        {
            hero.Equipment.Add("Armor");
            hero.Health += 100;
            hero.Gold -= this.Cost;
        }

        public ArmorEquiper(Hero hero) : base(hero) {
            this.Cost = 1200;
        }
    }
}
