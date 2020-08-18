using System;
using System.Collections.Generic;
using System.Text;

namespace HeroJourney.hero
{
    class WeaponEquiper : HeroEquiper
    {
        public override void equip()
        {
            hero.Equipment.Add("Weapon");
            hero.Damage += 25;
            hero.Gold -= this.Cost;
        }

        public WeaponEquiper(Hero hero) : base(hero){
            this.Cost = 1000;
        }
    }
}
