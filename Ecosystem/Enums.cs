using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public enum Death
    {
        ByPredator = 1,
        ByHunger = 2,
        ByAge = 3
    }
    public enum OperationChoiceHerbious
    {
        MoveToFood = 1,
        RunFromPredator = 2,
        SearchForPartner = 3,
        JustSwimming = 4
    }
    public enum OperationChoicePredator
    {
        MoveToFood = 1,
        SearchForPartner = 2,
        JustSwimming = 3,
    }
}
