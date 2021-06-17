using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Ecosystem
{
    public class Herb
    {
        private double _energyCurrent;
        private double _energyMax;
        private double _energyIncrease;
        private Cell _cell;
        public double EnergyCurrent
        {
            get
            {
                return _energyCurrent;
            }
            set
            {
                _energyCurrent = value;
            }
        }
        public double EnergyIncrease
        {
            get
            {
                _energyIncrease = Convert.ToDouble(ConfigurationManager.AppSettings["EnergyIncrease"]);
                return _energyIncrease;
            }
        }
        public double EnergyMax
        {
            get
            {
                return _energyMax;
            }
            set
            {
                _energyMax = value;
            }
        }
        public Cell Cell
        {
            get
            {
                return _cell;
            }
            set
            {
                _cell = value;
            }
        }
        public Herb()
        {
            EnergyMax = Convert.ToDouble(ConfigurationManager.AppSettings["EnergyMaxHerb"]);
            EnergyCurrent = HerbEnergyRandom();
        }
        private double HerbEnergyRandom()
        {
            Random random = new Random();
            return random.Next(10, 40);
        }
        public override string ToString()
        {
            return "G";
        }
    }
}
