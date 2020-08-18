using HeroJourney.enums;
using HeroJourney.hero;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroJourney.factories
{
    class HeroFactory
    {
        public Hero getHero(HeroTypes heroType) {

            switch (heroType)
            {
                case HeroTypes.Ironman: return new IronMan();
                case HeroTypes.Hulk: return new Hulk();
                case HeroTypes.Spiderman: return new Spiderman();
                default: return new CustomHero();
            }
        }
     
    }
}
