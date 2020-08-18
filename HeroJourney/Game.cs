using HeroJourney.factories;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;
using HeroJourney.enums;
using HeroJourney.hero;


namespace HeroJourney
{
    class Game


    {
    
    private static readonly int enemiesCount = 10;
    private static Hero myHero;
    private static ArrayList enemies = new ArrayList();
        private static EnemyTypes enemyTypes;
        private static HeroTypes heroTypes;
        private static ActionTypes actions;

    private static bool showEnemyStatsOption = false;
    private static bool customHeroOption = false;

    private static int winningGames = 0;
            

        public Game() { }

        //sets up the 2 available options (showing enemyStats and choosing the type of hero you want to play with
        public void gameSetup()
        {
            Console.WriteLine("Welcome! In this game you have to choose your brave hero that will fight " + enemiesCount + " enemies in order to save the princes!\n ");
            int optionSelected = 0;

            while (optionSelected != 1 && optionSelected != 2)
            {
                Console.WriteLine("Do you want to be able to see the stats of the enemy, before you choose your action?\n");
                Console.WriteLine("YES - press 1\n");
                Console.WriteLine("NO - press 2\n");

                optionSelected = Convert.ToInt32(Console.ReadLine());
                if (optionSelected == 1)
                {
                    showEnemyStatsOption = true; Console.WriteLine("You will see the enemy stats before you choose the action.\n");
                }
                if (optionSelected == 2)
                {
                    showEnemyStatsOption = false; Console.WriteLine("You will NOT see the enemy stats before you choose the action.\n");
                }
            }

            optionSelected = 0;

            while (optionSelected != 1 && optionSelected != 2)
            {
                Console.WriteLine("What kind of hero would you like?\n");
                Console.WriteLine("Customize my own stats - press 1\n");
                Console.WriteLine("A type that already exists - press 2\n");

                optionSelected = Convert.ToInt32(Console.ReadLine());
                if (optionSelected == 1)
                {
                    customHeroOption = true; Console.WriteLine("You can customize your own hero!.\n");
                }
                if (optionSelected == 2)
                {
                    customHeroOption = false; Console.WriteLine("You can choose one of the existing types of hero!\n");
                }
            }

        }

        //only decides, based on the customHeroOption, what type of hero to create
        public void createHero()
        {
            if (!customHeroOption) chooseYourHero();
            else customizeHero();
        }

        //choose iron man/hulk/spiderman
        public void chooseYourHero()
        {

            HeroFactory heroFactory = new HeroFactory();
            int heroType = 0;

          
            while (heroType <= 0 || heroType > Enum.GetNames(typeof(HeroTypes)).Length)
            {

                Console.WriteLine("Choose your favorite hero:\n");
                Console.WriteLine("Iron Man - press 1\n");
                Console.WriteLine("Hulk - press 2\n");
                Console.WriteLine("Spiderman - press 3\n");
                Console.WriteLine("-----------------------\n");
                Console.WriteLine("IronMan has more gold\n");
                Console.WriteLine("Hulk has more health\n");
                Console.WriteLine("Spiderman has more damage\n");

                heroType = Convert.ToInt32(Console.ReadLine());
            }

            myHero = heroFactory.getHero((HeroTypes)(heroType-1));

            switch (heroType)
            {
                case 1: { Console.WriteLine("Iron Man selected!\n"); break; }
                case 2: { Console.WriteLine("Hulk selected!\n"); break; }
                case 3: { Console.WriteLine("Spiderman selected!\n"); break; }
                default: { Console.WriteLine(); break; }
            }

            Console.WriteLine("Your hero's stats are:");
            myHero.showStats();

        }

        //customize the hero with your own stats
        public void customizeHero()
        {
            
            HeroFactory heroFactory = new HeroFactory();
            myHero = heroFactory.getHero(HeroTypes.Custom);

            int health = 0, damage = 0, gold = 0;
            string name = "";

            Console.WriteLine("Introduce the name of your hero: ");
            name = Console.ReadLine();

            while (health < 1 || health > 1000)
            {
                Console.WriteLine("Introduce " + name + "'s Health (between 1 to 1000): ");
                health = Convert.ToInt32(Console.ReadLine());
            }


            while (damage < 1 || damage > 100)
            {
                Console.WriteLine("Introduce " + name + "'s Damage (between 1 to 100): ");
                damage = Convert.ToInt32(Console.ReadLine());
            }


            while (gold < 1 || gold > 5000)
            {
                Console.WriteLine("Introduce " + name + "'s Gold (between 1 and 5000): ");
                gold = Convert.ToInt32(Console.ReadLine());
            }

            myHero.Health = health;
            myHero.Damage = damage;
            myHero.Gold = gold;
            myHero.Name = name;


            myHero.showStats();
        }

        //create the list of enemies
        public void createEnemies()
        {
            Console.WriteLine("Brace yourself! Enemies are being created!\n");

            EnemyFactory enemyFactory = new EnemyFactory();
            Random r = new Random();

            enemies.Clear();
            for (int i = 0; i < enemiesCount; i++)
            {
                //generate a random enemy, from the enemy types constant array
                int enemyType = r.Next(1, Enum.GetNames(typeof(EnemyTypes)).Length + 1);
                enemies.Add(enemyFactory.getEnemy((EnemyTypes)(enemyType - 1)));
            }
        }

        //this is the main method. It calls all the enemies one by one and lets you decide the action to take
        public void saveThePrincess()
        {


            for (int enemyCount = 0; enemyCount < enemies.Count; enemyCount++)
            {


                int equipOption = 0;
                Console.WriteLine("You can equip your hero with the following: ");
               if(myHero.Gold>=1000) Console.WriteLine("1- Weapon (gives 25 more damage). GOLD REQUIRED: 1000");
               if (myHero.Gold >= 1200) Console.WriteLine("2- Armor (gives 100 extra health). GOLD REQUIRED: 1200");
               if (myHero.Gold >= 800) Console.WriteLine("3- Shoes (provides 10% higher chances to sneak in). GOLD REQUIRED: 800");
                Console.WriteLine("4- Nothing ");

                equipOption = Convert.ToInt32(Console.ReadLine());
                HeroEquiper heroEquiper = null;

                switch (equipOption) {
                    case 1: { heroEquiper = new WeaponEquiper(myHero); Console.WriteLine("You equiped your hero with a weapon"); break; }
                    case 2: { heroEquiper = new ArmorEquiper(myHero); Console.WriteLine("You equiped your hero with an armor"); break; }
                    case 3: { heroEquiper = new ShoesEquiper(myHero); Console.WriteLine("You equiped your hero with shoes"); break; }
                    default: { break; }

                }

               if(heroEquiper!=null)
                    heroEquiper.equip();
                myHero.showEquipment();
                myHero.showStats();

                int action = 0;
                Enemy enemy = (Enemy)enemies[enemyCount];

                Console.WriteLine("You are fighting the enemy nr: " + (enemyCount + 1) + "\n");
                Console.WriteLine(enemy.showEnemyType());

                if (showEnemyStatsOption) enemy.showStats();

           

                while (action <= 0 || action > Enum.GetValues(typeof(ActionTypes)).Length)
                {
                    Console.WriteLine("Choose your action: \n");
                    foreach (string act in Enum.GetNames(typeof(ActionTypes)))
                    {
                        if (act.Equals("Bribe") && enemy.BribeAmount > myHero.Gold) Console.WriteLine("BRIBE OPTION NOT AVAILABLE!\n");
                        else
                            Console.WriteLine(act+ " - press " + (((int)Enum.Parse(typeof(ActionTypes), act) +1))+"\n");
                    }

                    action = Convert.ToInt32(Console.ReadLine());

                }

                switch (action)
                {
                    case 1: { myHero.fight(ref enemy); break; }
                    case 2: { myHero.bribe(ref enemy); break; }
                    case 3: { myHero.sneak(ref enemy); break; }
                    default: { myHero.showStats(); break; }
                }
                myHero.showStats();

                if (myHero.Health > 0)
                {
                    Console.WriteLine("Press Enter to get the next Enemy...\n");
                    Console.ReadLine();
                }

                if (myHero.Health <= 0)
                {
                    Console.WriteLine("YOU SUCK!" + myHero.Name + " COULDN'T SAVE THE PRINCES");
                    break;
                }


            }

            if (myHero.Health > 0) Console.WriteLine("CONGRATULATIONS! YOU SAVED THE PRINCESS!");



        }

        //the automation for the best decision to take to win the game. still on progress

        //public int runGame(int runTimes, int type)
        //{

        //    HeroFactory factory = new HeroFactory();
        //    winningGames = 0;
        //    for (int i = 0; i < runTimes; i++)
        //    {
        //        myHero = factory.getHero((HeroTypes)(type));
        //        this.createEnemies();
        //        this.automateDecisions9();

        //    }
        //    Console.WriteLine("\nYou won the game " + winningGames + " times");
        //    return winningGames;
        //}

        //public void automateDecisions1()
        //{

        //    for (int enemyCount = 0; enemyCount < enemies.Count; enemyCount++)
        //    {

        //        Enemy enemy = (Enemy)enemies[enemyCount];

        //        if (showEnemyStatsOption) enemy.showStats();

        //        Console.WriteLine("You are fighting the enemy nr: " + (enemyCount + 1) + "\n");
        //        Console.WriteLine(enemy.showEnemyType());

        //        if (enemy.Clumsiness >= 75) myHero.sneak(ref enemy);
        //        else if (enemy.BribeAmount < myHero.Gold / 10) myHero.bribe(ref enemy);
        //        else myHero.fight(ref enemy);

        //        myHero.showStats();

        //        if (myHero.Health <= 0)
        //        {
        //            Console.WriteLine("YOU SUCK!" + myHero.Name + " COULDN'T SAVE THE PRINCES");
        //            break;
        //        }

        //    }

        //    if (myHero.Health > 0)
        //    {
        //        winningGames += 1;
        //        Console.WriteLine("CONGRATULATIONS! YOU SAVED THE PRINCESS!");
        //    }

        //}

        //public void automateDecisions2()
        //{

        //    for (int enemyCount = 0; enemyCount < enemies.Count; enemyCount++)
        //    {

        //        Enemy enemy = (Enemy)enemies[enemyCount];

        //        if (showEnemyStatsOption) enemy.showStats();

        //        Console.WriteLine("You are fighting the enemy nr: " + (enemyCount + 1) + "\n");
        //        Console.WriteLine(enemy.showEnemyType());


        //        if (enemyCount < 5)
        //        {
        //            if (enemy.Clumsiness >= 85) myHero.sneak(ref enemy);
        //            else if (enemy.BribeAmount < myHero.Gold / 10) myHero.bribe(ref enemy);
        //            else myHero.fight(ref enemy);
        //        }
        //        else
        //        {

        //            if (enemy.BribeAmount < myHero.Gold / 30) myHero.bribe(ref enemy);
        //            else if (enemy.Clumsiness >= 80) myHero.sneak(ref enemy);
        //            else myHero.fight(ref enemy);

        //        }

        //        myHero.showStats();

        //        if (myHero.Health <= 0)
        //        {
        //            Console.WriteLine("YOU SUCK!" + myHero.Name + " COULDN'T SAVE THE PRINCES");
        //            break;
        //        }

        //    }

        //    if (myHero.Health > 0)
        //    {
        //        winningGames += 1;
        //        Console.WriteLine("CONGRATULATIONS! YOU SAVED THE PRINCESS!");
        //    }

        //}


        //public void automateDecisions3()
        //{

        //    for (int enemyCount = 0; enemyCount < enemies.Count; enemyCount++)
        //    {

        //        Enemy enemy = (Enemy)enemies[enemyCount];

        //        if (showEnemyStatsOption) enemy.showStats();

        //        Console.WriteLine("You are fighting the enemy nr: " + (enemyCount + 1) + "\n");
        //        Console.WriteLine(enemy.showEnemyType());


        //        if (enemy.BribeAmount < 500 && myHero.Gold > 1500) myHero.bribe(ref enemy);
        //        else if (enemy.Clumsiness >= 80) myHero.sneak(ref enemy);
        //        else myHero.fight(ref enemy);



        //        myHero.showStats();

        //        if (myHero.Health <= 0)
        //        {
        //            Console.WriteLine("YOU SUCK!" + myHero.Name + " COULDN'T SAVE THE PRINCES");
        //            break;
        //        }

        //    }

        //    if (myHero.Health > 0)
        //    {
        //        winningGames += 1;
        //        Console.WriteLine("CONGRATULATIONS! YOU SAVED THE PRINCESS!");
        //    }

        //}

        //public void automateDecisions4()
        //{

        //    for (int enemyCount = 0; enemyCount < enemies.Count; enemyCount++)
        //    {

        //        Enemy enemy = (Enemy)enemies[enemyCount];

        //        if (showEnemyStatsOption) enemy.showStats();

        //        Console.WriteLine("You are fighting the enemy nr: " + (enemyCount + 1) + "\n");
        //        Console.WriteLine(enemy.showEnemyType());

        //        if (enemyCount > 7)
        //        { 
        //            if (enemy.BribeAmount < myHero.Gold) myHero.bribe(ref enemy);
        //        }
        //        else
        //        {

        //            if (enemy.BribeAmount < 700 && myHero.Gold > 1500) myHero.bribe(ref enemy);
        //            else if (enemy.Clumsiness >= 80) myHero.sneak(ref enemy);
        //            else myHero.fight(ref enemy);
        //        }


        //        myHero.showStats();

        //        if (myHero.Health <= 0)
        //        {
        //            Console.WriteLine("YOU SUCK!" + myHero.Name + " COULDN'T SAVE THE PRINCES");
        //            break;
        //        }

        //    }

        //    if (myHero.Health > 0)
        //    {
        //        winningGames += 1;
        //        Console.WriteLine("CONGRATULATIONS! YOU SAVED THE PRINCESS!");
        //    }

        //}

        //public void automateDecisions5()
        //{

        //    for (int enemyCount = 0; enemyCount < enemies.Count; enemyCount++)
        //    {

        //        Enemy enemy = (Enemy)enemies[enemyCount];

        //        if (showEnemyStatsOption) enemy.showStats();

        //        Console.WriteLine("You are fighting the enemy nr: " + (enemyCount + 1) + "\n");
        //        Console.WriteLine(enemy.showEnemyType());

        //        if (enemyCount > 5)
        //        {
        //            if (enemy.BribeAmount < myHero.Gold) myHero.bribe(ref enemy);
        //        }
        //        else
        //        {

        //            if (enemy.BribeAmount < 700 && myHero.Gold > 1500) myHero.bribe(ref enemy);
        //            else if (enemy.BribeAmount < 300) myHero.bribe(ref enemy);
        //            else if (enemy.Clumsiness >= 80) myHero.sneak(ref enemy);
        //            else myHero.fight(ref enemy);
        //        }


        //        myHero.showStats();

        //        if (myHero.Health <= 0)
        //        {
        //            Console.WriteLine("YOU SUCK!" + myHero.Name + " COULDN'T SAVE THE PRINCES");
        //            break;
        //        }

        //    }

        //    if (myHero.Health > 0)
        //    {
        //        winningGames += 1;
        //        Console.WriteLine("CONGRATULATIONS! YOU SAVED THE PRINCESS!");
        //    }

        //}

        //public void automateDecisions6()
        //{

        //    for (int enemyCount = 0; enemyCount < enemies.Count; enemyCount++)
        //    {

        //        Enemy enemy = (Enemy)enemies[enemyCount];

        //        if (showEnemyStatsOption) enemy.showStats();

        //        Console.WriteLine("You are fighting the enemy nr: " + (enemyCount + 1) + "\n");
        //        Console.WriteLine(enemy.showEnemyType());


        //        if (enemy.BribeAmount > 700 && enemy.BribeAmount < myHero.Gold) {
        //            if (enemy.Damage < 15 || enemy.Health < myHero.Damage) myHero.fight(ref enemy);
        //            else myHero.bribe(ref enemy);
        //        }
        //        else if (enemy.BribeAmount < myHero.Gold) myHero.bribe(ref enemy);
        //        else if (enemy.Clumsiness >= 80) myHero.sneak(ref enemy);
        //        else myHero.fight(ref enemy);
                


        //        myHero.showStats();

        //        if (myHero.Health <= 0)
        //        {
        //            Console.WriteLine("YOU SUCK!" + myHero.Name + " COULDN'T SAVE THE PRINCES");
        //            break;
        //        }

        //    }

        //    if (myHero.Health > 0)
        //    {
        //        winningGames += 1;
        //        Console.WriteLine("CONGRATULATIONS! YOU SAVED THE PRINCESS!");
        //    }

        //}

        //public void automateDecisions7()
        //{

        //    for (int enemyCount = 0; enemyCount < enemies.Count; enemyCount++)
        //    {

        //        Enemy enemy = (Enemy)enemies[enemyCount];

        //        if (showEnemyStatsOption) enemy.showStats();

        //        Console.WriteLine("You are fighting the enemy nr: " + (enemyCount + 1) + "\n");
        //        Console.WriteLine(enemy.showEnemyType());


        //        int initialGold = myHero.Gold;
        //        if (initialGold >= 3500)
        //        {
        //            if (myHero.Gold > enemy.BribeAmount) myHero.bribe(ref enemy);
        //            else if (enemy.Clumsiness >= 80) myHero.sneak(ref enemy);
        //            else myHero.fight(ref enemy);
        //        }
        //        else
        //        {
        //            if (enemy.Clumsiness >= 75 && enemy.Damage < 20) myHero.sneak(ref enemy);
        //            else if (enemy.Clumsiness >= 85) myHero.sneak(ref enemy);
        //            else if (enemy.BribeAmount < 350) myHero.bribe(ref enemy);
        //            else myHero.fight(ref enemy);

        //        }



               


        //        myHero.showStats();

        //        if (myHero.Health <= 0)
        //        {
        //            Console.WriteLine("YOU SUCK!" + myHero.Name + " COULDN'T SAVE THE PRINCES");
        //            break;
        //        }

        //    }

        //    if (myHero.Health > 0)
        //    {
        //        winningGames += 1;
        //        Console.WriteLine("CONGRATULATIONS! YOU SAVED THE PRINCESS!");
        //    }

        //}

        //public void automateDecisions8()
        //{

        //    for (int enemyCount = 0; enemyCount < enemies.Count; enemyCount++)
        //    {

        //        Enemy enemy = (Enemy)enemies[enemyCount];

        //        if (showEnemyStatsOption) enemy.showStats();

        //        Console.WriteLine("You are fighting the enemy nr: " + (enemyCount + 1) + "\n");
        //        Console.WriteLine(enemy.showEnemyType());


        //        int damageTaken;
        //        damageTaken = calculateDamageTaken(myHero,enemy);
        //        if (damageTaken > myHero.Health)
        //        {
        //            if (myHero.Gold > enemy.BribeAmount) myHero.bribe(ref enemy);
        //            else myHero.sneak(ref enemy);
        //        }
        //        else if(damageTaken*20<=myHero.Health)
        //        {
        //            //directly fight if i lose 5% of my health or less 
        //            myHero.fight(ref enemy);

        //        }
        //        else if (damageTaken * 10 <= myHero.Health)
        //        {
        //            //i bribe if i dont spend more than 25%
        //            if (myHero.Gold > enemy.BribeAmount * 4) myHero.bribe(ref enemy);
        //            //i bribe if im in the last eneimes already, even with 50% cost
        //            else if (myHero.Gold > enemy.BribeAmount * 2 && enemiesCount > 6) myHero.bribe(ref enemy);
        //            else if (enemy.Clumsiness >= 85) myHero.sneak(ref enemy);
        //            else myHero.fight(ref enemy);
        //        }
        //        else if (damageTaken * 4 <= myHero.Health)
        //        {
        //            //i bribe if i dont spend more than 50%
        //            if (myHero.Gold > enemy.BribeAmount * 2) myHero.bribe(ref enemy);
        //            //i bribe if im in the last eneimes already, even if i spend it all
        //            else if (myHero.Gold > enemy.BribeAmount && enemiesCount > 8) myHero.bribe(ref enemy);
        //            else if (enemy.Clumsiness >= 90) myHero.sneak(ref enemy);
        //            else myHero.fight(ref enemy);
        //        }
        //        else
        //        {
        //            if (myHero.Gold > enemy.BribeAmount) myHero.bribe(ref enemy);
        //            else if (enemy.Clumsiness >= 80) myHero.sneak(ref enemy);
        //            else myHero.fight(ref enemy);
        //        }


        //        myHero.showStats();

        //        if (myHero.Health <= 0)
        //        {
        //            Console.WriteLine("YOU SUCK!" + myHero.Name + " COULDN'T SAVE THE PRINCES");
        //            break;
        //        }

        //    }

        //    if (myHero.Health > 0)
        //    {
        //        winningGames += 1;
        //        Console.WriteLine("CONGRATULATIONS! YOU SAVED THE PRINCESS!");
        //    }

        //}

        //public void automateDecisions9()
        //{

        //    for (int enemyCount = 0; enemyCount < enemies.Count; enemyCount++)
        //    {

        //        Enemy enemy = (Enemy)enemies[enemyCount];

        //        if (showEnemyStatsOption) enemy.showStats();

        //        Console.WriteLine("You are fighting the enemy nr: " + (enemyCount + 1) + "\n");
        //        Console.WriteLine(enemy.showEnemyType());


        //        int damageTaken;
        //        damageTaken = calculateDamageTaken(myHero, enemy);

        //        if (damageTaken > myHero.Health)
        //        {
        //            if (myHero.Gold > enemy.BribeAmount) myHero.bribe(ref enemy);
        //            else myHero.sneak(ref enemy);
        //        }
        //        else if (enemyCount < 3)
        //        {
        //            if (damageTaken * 20 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 20 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 90) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else if (damageTaken * 10 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 7 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 90) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else if (damageTaken * 7 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 5 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 85) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else if (damageTaken * 5 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 3 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 85) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else
        //            {
        //                if (enemy.BribeAmount * 2.5 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 80) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }


        //        }
        //        else if (enemyCount < 5)
        //        {
        //            if (damageTaken * 15 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 7 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 90) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else if (damageTaken * 7 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 5 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 90) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else if (damageTaken * 5 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 4 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 85) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else if (damageTaken * 4 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 3 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 85) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else
        //            {
        //                if (enemy.BribeAmount * 2 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 80) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }


        //        }
        //        else if (enemyCount < 9)
        //        {
        //            if (myHero.Gold >= 1000 * (enemiesCount - enemyCount+1)) myHero.bribe(ref enemy);
        //            else
        //            if (damageTaken * 10 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 10 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 85) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else if (damageTaken * 5 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 4 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 85) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else if (damageTaken * 3 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 2.5 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 80) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else if (damageTaken * 2.5 < myHero.Health)
        //            {
        //                if (enemy.BribeAmount * 2 <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 80) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }
        //            else
        //            {
        //                if (enemy.BribeAmount <= myHero.Gold) myHero.bribe(ref enemy);
        //                else if (enemy.Clumsiness > 75) myHero.sneak(ref enemy);
        //                else myHero.fight(ref enemy);
        //            }


        //        }
        //        else 
        //        {
        //            if (damageTaken < myHero.Health) myHero.fight(ref enemy);
        //            else if (myHero.Gold > enemy.BribeAmount) myHero.bribe(ref enemy);
        //            else myHero.sneak(ref enemy);
                
        //        }



        //        myHero.showStats();

        //        if (myHero.Health <= 0)
        //        {
        //            Console.WriteLine("YOU SUCK!" + myHero.Name + " COULDN'T SAVE THE PRINCES");
        //            break;
        //        }

        //    }

        //    if (myHero.Health > 0)
        //    {
        //        winningGames += 1;
        //        Console.WriteLine("CONGRATULATIONS! YOU SAVED THE PRINCESS!");
        //    }

        //}

        public int calculateDamageTaken(Hero myHero, Enemy enemy)
        {

            int hits = (int)Math.Ceiling(Convert.ToDecimal(enemy.Health / Convert.ToDouble(myHero.Damage)));
            return enemy.Damage * hits;
        }

    }
}
