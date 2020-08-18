using HeroJourney.factories;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;

namespace HeroJourney
{
    class Program
    {
       

        static void Main(string[] args)
        {

            Game game = new Game();
            game.gameSetup();
            game.createHero();
            game.createEnemies();
            game.saveThePrincess();


          



        }

    }
}
    

