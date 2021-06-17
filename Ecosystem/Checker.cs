using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public class Checker
    {
        public void CheckInputedInfo(int cellQuantity, int objectQuantity)
        {
            if(cellQuantity < objectQuantity)
            {
                Console.WriteLine("Sum of inputed objects is bigger than quantity of cells");
                Console.ReadKey();
                Environment.Exit(1);

            }
        }
        public bool CheckIsInteger(string value)
        {
            if(Convert.ToInt32(value) < 0)
            {
                return false;
            }
            return true;
        }
    }
}
