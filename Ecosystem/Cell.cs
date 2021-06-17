using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public class Cell
    {
        private int _x;
        private int _y;
        private Fish _fish;
        private Herb _herb;
        private Stone _stone;
        public Fish Fish
        {
            get
            {
                return _fish;
            }
            set
            {
                _fish = value;
            }
        }
        public Herb Herb
        {
            get
            {
                return _herb;
            }
            set
            {
                _herb = value;
            }
        }
        public Stone Stone
        {
            get
            {
                return _stone;
            }
            set
            {
                _stone = value;
            }
        }
        public int CoordY
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
        public int CoordX
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        public bool GetIsAvaiable()
        {
            if(_fish == null && _herb == null && _stone == null)
            {
                return true;
            }
            return false;
        }

        public object GetContentByCoords(int x, int y)
        {
            if(_fish != null)
            {
                if (_fish.Cell.CoordX == x && _fish.Cell.CoordY == y)
                {
                    return _fish;
                }
            }
            else if (_herb != null)
            {
                if (_herb.Cell.CoordX == x && _herb.Cell.CoordY == y)
                {
                    return _herb;
                }
                
            }
            else if(_stone != null)
            {
                if (_stone.Cell.CoordX == x && _stone.Cell.CoordY == y)
                {
                    return _stone;
                }
            }


            return null;
        }

        public void SelfClean()
        {
            _fish = null;
            _herb = null;
            _stone = null;
        }
        public override string ToString()
        {
            if(_fish != null)
            {
                return _fish.ToString();
            }
            else if (_herb != null)
            {
                return _herb.ToString();
            }
            else if (_stone != null)
            {
                return _stone.ToString();
            }
            return ".";
        }
    }
}
