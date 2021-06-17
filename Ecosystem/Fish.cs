using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public abstract class Fish
    {
        private int _ageCurrent;
        private int _ageMax;
        private int _ageBeginMature;
        private int _ageEndMature;
        private int _moveMaxLenght;
        private int _moveCurrentLenght;
        private int _observeRadius;
        private double _energyCurrent;
        private double _energyMax;
        private double _energyHungryLvl;
        private double _energyIterationDecrease;
        private int _pregancyCurrent;
        private int _pregancyLenght;
        private bool _isMale;
        private Death _deathType;
        private Cell _cell;
        private Cell[,] _visibleCellsArray;
        public Cell[,] VisibleCells
        {
            get
            {
                return _visibleCellsArray;
            }
            set
            {
                _visibleCellsArray = value;
            }

        }
        public int AgeCurrent
        {
            get
            {
                return _ageCurrent;
            }
            set
            {
                _ageCurrent = value;
            }

        }
        public int AgeMax
        {
            get
            {
                return _ageMax;
            }
            set
            {
                _ageMax = value;
            }

        }
        public int AgeBeginMature
        {
            get
            {
                return _ageBeginMature;
            }
            set
            {
                _ageBeginMature = value;
            }

        }
        public int AgeEndMature
        {
            get
            {
                return _ageEndMature;
            }
            set
            {
                _ageEndMature = value;
            }

        }
        public int MoveMaxLenght
        {
            get
            {
                return _moveMaxLenght;
            }
            set
            {
                _moveMaxLenght = value;
            }

        }
        public int MoveCurrentLenght
        {
            get
            {
                return _moveCurrentLenght;
            }
            set
            {
                _moveCurrentLenght = value;
            }

        }
        public int ObserveRadius
        {
            get
            {
                return _observeRadius;
            }
            set
            {
                _observeRadius = value;
            }

        }
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
        public double EnergyHungryLvl
        {
            get
            {
                return _energyHungryLvl;
            }
            set
            {
                _energyHungryLvl = value;
            }

        }
        public double EnergyIterationDecrease
        {
            get
            {
                return _energyIterationDecrease;
            }
            set
            {
                _energyIterationDecrease = value;
            }

        }
        public int PregancyCurrent
        {
            get
            {
                return _pregancyCurrent;
            }
            set
            {
                _pregancyCurrent = value;
            }
        }
        public int PregancyLenght
        {
            get
            {
                return _pregancyLenght;
            }
            set
            {
                _pregancyLenght = value;
            }

        }
        public bool IsMale
        {
            get
            {
                return _isMale;
            }
            set
            {
                _isMale = value;
            }
        }
        public Death DeathType
        {
            get
            {
                return _deathType;
            }
            set
            {
                _deathType = value;
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
        public bool IsPregant
        {
            get
            {
                int termDiffetence = _pregancyLenght - _pregancyCurrent;
                if (termDiffetence == _pregancyLenght)
                {
                    return false;
                }
                return true;
            }
        }
        // Check is another fish can be a partner for reprodaction, is yes StartPregancy() is called and return true, if not return false
        public bool CheckAbilityToReproduction(Fish partner)
        {
            if((_isMale == true && partner._isMale == true) || (_isMale == false && partner._isMale == false) || partner.IsPregant || IsPregant)
            {
                return false;
            }
            return true;
        }
        //Method for PregancyCurrent = 1
        public void StartPregancy(Fish partner)
        {
            if(partner.IsMale == true)
            {
                PregancyCurrent = 1;
            }
            partner.PregancyCurrent = 1;
        }
        //Method for creating new instance of fish
        public abstract void Reproduction();
        public abstract void SetDeath();
    }
}
