using System;
using System.Collections.Generic;
using System.Text;

namespace HeroJourney.hero
{
   abstract class HeroEquiper:Hero
    {

        protected Hero hero;
        private int cost;

        public int Cost { get; set; }

        public HeroEquiper(Hero hero) {
            this.hero = hero;
        }

        public abstract void equip();
    }
}
