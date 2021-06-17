using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public class QuantityInputer
    {
        private Checker checker = new Checker();
        public int InputFishHerbivorousQuantity()
        {
            while (true)
            {
                Console.WriteLine("Input quantity of Herbivorous Fish:");
                string quantity = Console.ReadLine();
                try
                {
                    if (checker.CheckIsInteger(quantity))
                    {
                        return Convert.ToInt32(quantity);
                    }
                    throw new InvalidCastException();
                }
                catch
                {
                    Console.WriteLine("Input a positive number quantity please:");
                }
            }
        }
        public int InputFishPredatorQuantity()
        {
            while (true)
            {
                Console.WriteLine("Input quantity of Predator Fish:");
                string quantity = Console.ReadLine();
                try
                {
                    return Convert.ToInt32(quantity);
                }
                catch
                {
                    Console.WriteLine("Input a number quantity please:");
                }
            }
        }
        public int InputHerbsQuantity()
        {
            while (true)
            {
                Console.WriteLine("Input quantity of Herbs:");
                string quantity = Console.ReadLine();
                try
                {
                    return Convert.ToInt32(quantity);
                }
                catch
                {
                    Console.WriteLine("Input a number quantity please:");
                }
            }
        }
        public int InputStonesQuantity()
        {
            while (true)
            {
                Console.WriteLine("Input quantity of Stones:");
                string quantity = Console.ReadLine();
                try
                {
                    return Convert.ToInt32(quantity);
                }
                catch
                {
                    Console.WriteLine("Input a number quantity please:");
                }
            }
        }
    }
}
