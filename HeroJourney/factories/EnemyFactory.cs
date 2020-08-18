using System;
using System.Collections.Generic;
using System.Text;
using HeroJourney.enums;

namespace HeroJourney.factories
{
    class EnemyFactory
    {
        public Enemy getEnemy(EnemyTypes enemyType) { 
            switch(enemyType)
            {
                case EnemyTypes.Ogre: return new Ogre();
                case EnemyTypes.Bandit: return new Bandit();
                case EnemyTypes.Champion:return new Champion();
                default: return null;
            }
        
        }

    }
}
