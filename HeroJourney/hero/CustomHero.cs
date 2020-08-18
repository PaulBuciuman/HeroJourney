using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace HeroJourney.hero
{
    class CustomHero : Hero
    {
        private string name;
        public new string Name { set =>name = value; }

        public CustomHero() { }
        public CustomHero(int health, int damage,int gold) {
            this.Health = health;
            this.Damage = damage;
            this.Gold = gold;
        }
    }
}
