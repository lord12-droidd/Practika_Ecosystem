using System;

namespace Ecosystem
{
    class Program
    {
        static void Main(string[] args)
        {
            QuantityInputer quantityInputer = new QuantityInputer();
            Checker checker = new Checker();
            Aquarium aquarium = new Aquarium();
            FishPredator fishPredator = new FishPredator();

            int fishHerbQuantity = quantityInputer.InputFishHerbivorousQuantity();
            int fishPredQuantity = quantityInputer.InputFishPredatorQuantity();
            int stonesQuantity = quantityInputer.InputStonesQuantity();
            int herbsQuantity = quantityInputer.InputHerbsQuantity();
            checker.CheckInputedInfo(aquarium.CellsQuantity, herbsQuantity + stonesQuantity + fishHerbQuantity + fishPredQuantity);
            aquarium.FishHerbQuantity = fishHerbQuantity;
            aquarium.FishPredatorQuantity = fishPredQuantity;
            aquarium.HerbsQuantity = herbsQuantity;
            aquarium.StonesQuantity = stonesQuantity;
            aquarium.StartEcosystem();
            string stringToExit;
            while (true)
            {
                Console.WriteLine("Input 1 to exit:");
                stringToExit = Console.ReadLine();
                if(stringToExit == "1")
                {
                    break;
                }
                aquarium.NextIteration();
                Console.ReadKey();
            }


            
            Console.WriteLine("Hello World!");
        }
    }
}
