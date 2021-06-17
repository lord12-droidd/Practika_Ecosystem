using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public class Stone
    {
        private Cell _cell;
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
        public override string ToString()
        {
            return "X";
        }
    }
}
