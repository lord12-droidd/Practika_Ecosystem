using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;


namespace Ecosystem
{
    public class FishPredator : Fish
    {
        private OperationChoicePredator _operation;
        public OperationChoicePredator Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
            }
        }
        public FishPredator()
        {
            AgeCurrent = 0;
            AgeMax = Convert.ToInt32(ConfigurationManager.AppSettings["AgeMax"]);
            AgeBeginMature = Convert.ToInt32(ConfigurationManager.AppSettings["AgeBeginMature"]);
            AgeEndMature = Convert.ToInt32(ConfigurationManager.AppSettings["AgeEndMature"]);
            MoveMaxLenght = Convert.ToInt32(ConfigurationManager.AppSettings["MoveMaxLenght"]);
            ObserveRadius = Convert.ToInt32(ConfigurationManager.AppSettings["ObserveRadiusPredator"]);
            EnergyMax = Convert.ToInt32(ConfigurationManager.AppSettings["EnergyMax"]);
            EnergyCurrent = EnergyMax;
            EnergyIterationDecrease = Convert.ToInt32(ConfigurationManager.AppSettings["EnergyDecreaseOnIteration"]);
            EnergyHungryLvl = Convert.ToInt32(ConfigurationManager.AppSettings["EnergyHungryLvl"]);
            IsMale = GenderRandom();
            if(IsMale == false)
            {
                PregancyLenght = Convert.ToInt32(ConfigurationManager.AppSettings["PregancyLenth"]);
            }
            VisibleCells = new Cell[ObserveRadius * 2, ObserveRadius * 2];
            Operation = OperationChoicePredator.JustSwimming;
        }
        public override void Reproduction()
        {
            
        }

        public override void SetDeath()
        {
            if(AgeCurrent == AgeMax)
            {
                DeathType = Death.ByAge;
            }
            else if(EnergyCurrent == 0)
            {
                DeathType = Death.ByHunger;
            }
        }

        private bool GenderRandom()
        {
            Random random = new Random();
            if(random.Next(0,2) == 0)
            {
                return true;
            }
            return false;
        }
        public double Eat(FishHerbious fish)
        {
            EnergyCurrent += fish.EnergyCurrent;
            fish.DeathType = Death.ByPredator;
            fish.Cell.Fish = null;
            if (EnergyCurrent >= EnergyMax)
            {
                EnergyCurrent = EnergyMax;
            }
            return EnergyCurrent;
        }
        private Cell GetCellWithFood()
        {
            for (int x = 0; x < ObserveRadius * 2; x++)
            {
                for (int y = 0; y < ObserveRadius * 2; y++)
                {
                    if (VisibleCells[x, y] == null)
                    {
                        continue;
                    }
                    if (VisibleCells[x, y].Fish != null && VisibleCells[x, y].Fish.GetType().Equals(typeof(FishHerbious)))
                    {
                        return VisibleCells[x, y];
                    }
                }
            }
            return null;
        }
        public void Move()
        {
            if (Operation == OperationChoicePredator.MoveToFood)
            {

                Cell foodCell = GetCellWithFood();
                if (foodCell == null)
                {
                    return;
                }
                Cell avaibleCell = GetNonNullCellFromAvaiableCells(GetAvCells(foodCell));

                int oldX = Cell.CoordX;
                int oldY = Cell.CoordY;
                Cell.CoordX = avaibleCell.CoordX;
                Cell.CoordY = avaibleCell.CoordY;
                Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = this;

                if (ISFishMoved(oldX, oldY))
                {
                    Aquarium.CellsArray[oldX, oldY].SelfClean();
                    Eat((FishHerbious)foodCell.Fish);
                    return;

                }
                Eat((FishHerbious)foodCell.Fish);
                return;
            }
            else if (Operation == OperationChoicePredator.SearchForPartner)
            {
                Cell partnerCell = GetCellWithPartner();
                if (partnerCell == null)
                {
                    return;
                }
                Cell avaibleCell = GetNonNullCellFromAvaiableCells(GetAvCells(partnerCell));
                int oldX = Cell.CoordX;
                int oldY = Cell.CoordY;
                Cell.CoordX = avaibleCell.CoordX;
                Cell.CoordY = avaibleCell.CoordY;
                Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = this;

                if (ISFishMoved(oldX, oldY))
                {
                    Aquarium.CellsArray[oldX, oldY].SelfClean();
                    return;

                }
                Cell.SelfClean();
                StartPregancy(Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish);
                return;
            }
            else if (Operation == OperationChoicePredator.JustSwimming)
            {
                int oldX = Cell.CoordX;
                int oldY = Cell.CoordY;
                var cell = GetAvCells(Cell);
                Cell cellToMove = GetNonNullCellFromAvaiableCells(cell);
                if (cellToMove.GetIsAvaiable())
                {

                    cellToMove.Fish = this;
                    Cell.CoordX = cellToMove.CoordX;
                    Cell.CoordY = cellToMove.CoordY;
                    Cell.SelfClean();
                    Aquarium.CellsArray[cellToMove.CoordX, cellToMove.CoordY] = cellToMove;
                    Aquarium.CellsArray[oldX, oldY].SelfClean();
                }
                return;
            }
        }
        private Cell GetCellWithPartner()
        {
            for (int x = 0; x < ObserveRadius * 2; x++)
            {
                for (int y = 0; y < ObserveRadius * 2; y++)
                {
                    if (VisibleCells[x, y] == null)
                    {
                        continue;
                    }
                    if (VisibleCells[x, y].Fish != null)
                    {
                        if (VisibleCells[x, y].Fish.GetType().Equals(typeof(FishPredator)) && VisibleCells[x, y].Fish.CheckAbilityToReproduction(VisibleCells[x, y].Fish))
                        {
                            StartPregancy(VisibleCells[x, y].Fish);
                            return VisibleCells[x, y];
                        }
                    }
                }
            }
            return null;
        }
        private Cell[,] GetAvCells(Cell cell)
        {
            Cell[,] avCells = new Cell[3, 3];
            for (int x = cell.CoordX - 1; x < cell.CoordX + 2; x++)
            {
                for (int y = cell.CoordY - 1; y < cell.CoordY + 2; y++)
                {
                    if (x - cell.CoordX + 1 >= 3 || y - cell.CoordY + 1 >= 3 || x - cell.CoordX + 1 < 0 || y - cell.CoordY + 1 < 0)
                    {
                        continue;
                    }
                    if (y > Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumHeight"]) - 1 || x > Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumWidght"]) - 1)
                    {
                        if (x > 0 && y > 0)
                        {
                            if (Aquarium.CellsArray[x - 1, y - 1] == null)
                            {
                                continue;
                            }
                            continue;
                        }
                        else if (x > Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumWidght"]) - 1 && y >= 0)
                        {
                            if (Aquarium.CellsArray[x - 1, y] == null)
                            {
                                continue;
                            }
                        }
                        else if (y > Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumHeight"]) - 1 && x >= 0)
                        {
                            if (Aquarium.CellsArray[x, y - 1] == null)
                            {
                                continue;
                            }
                        }
                        continue;
                    }
                    else if (x < 0 || y < 0)
                    {
                        if (x < 0 && y < 0)
                        {
                            if (Aquarium.CellsArray[0, 0] == null)
                            {
                                continue;
                            }
                        }
                        else if (x < 0)
                        {
                            if (Aquarium.CellsArray[0, y] == null)
                            {
                                continue;
                            }
                        }
                        else if (y < 0)
                        {
                            if (Aquarium.CellsArray[x, 0] == null)
                            {
                                continue;
                            }
                        }

                        continue;
                    }

                    if (Aquarium.CellsArray[x, y] == null)
                    {
                        continue;
                    }
                    if (!VisibleCells[x - cell.CoordX + 1, y - cell.CoordY + 1].GetIsAvaiable())
                    {
                        continue;
                    }
                    avCells[x - cell.CoordX + 1, y - cell.CoordY + 1] = Aquarium.CellsArray[x, y];
                }
            }
            return avCells;
        }
        private bool ISFishMoved(int x, int y)
        {
            if (Cell.CoordX == x && Cell.CoordY == y)
            {
                return false;
            }
            return true;
        }
        private Cell GetNonNullCellFromAvaiableCells(Cell[,] avaiableCells)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (avaiableCells[x, y] != null && avaiableCells[x, y].GetIsAvaiable())
                    {
                        return avaiableCells[x, y];
                    }
                }
            }
            return Cell;
        }
        public void OperationAnalysis()
        {
            if (EnergyCurrent <= EnergyHungryLvl)
            {
                Operation = OperationChoicePredator.MoveToFood;
                return;
            }
            else if (AgeCurrent >= AgeBeginMature && AgeCurrent <= AgeEndMature && (!IsPregant))
            {
                Operation = OperationChoicePredator.SearchForPartner;
                return;
            }
            Operation = OperationChoicePredator.JustSwimming;
        }
        public override string ToString()
        {
            string fish = "P";
            if (IsMale)
            {
                fish += "M";
            }
            else
            {
                fish += "F";
                if (IsPregant)
                {
                    fish += "+";
                }
                else
                {
                    fish += "-";
                }
            }

            if (AgeCurrent < AgeBeginMature)
            {
                fish += "1";
            }
            else if (AgeCurrent >= AgeBeginMature && AgeCurrent <= AgeEndMature)
            {
                fish += "2";
            }
            else
            {
                fish += "3";
            }
            return fish;
        }
    }
}
