using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Ecosystem
{
    public class FishHerbious : Fish
    {
        private OperationChoiceHerbious _operation;

        private bool _isEaten;
        public bool IsEaten
        {
            get
            {
                return _isEaten;
            }
            set
            {
                _isEaten = value;
            }
        }
        public OperationChoiceHerbious Operation
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
        public FishHerbious()
        {
            AgeCurrent = 0;
            AgeMax = Convert.ToInt32(ConfigurationManager.AppSettings["AgeMax"]);
            AgeBeginMature = Convert.ToInt32(ConfigurationManager.AppSettings["AgeBeginMature"]);
            AgeEndMature = Convert.ToInt32(ConfigurationManager.AppSettings["AgeEndMature"]);
            MoveMaxLenght = Convert.ToInt32(ConfigurationManager.AppSettings["MoveMaxLenght"]);
            ObserveRadius = Convert.ToInt32(ConfigurationManager.AppSettings["ObserveRadiusHerbivorous"]);
            EnergyMax = Convert.ToInt32(ConfigurationManager.AppSettings["EnergyMax"]);
            EnergyCurrent = EnergyMax;
            EnergyHungryLvl = Convert.ToInt32(ConfigurationManager.AppSettings["EnergyHungryLvl"]);
            EnergyIterationDecrease = Convert.ToInt32(ConfigurationManager.AppSettings["EnergyDecreaseOnIteration"]);
            IsMale = GenderRandom();
            if (IsMale == false)
            {
                PregancyLenght = Convert.ToInt32(ConfigurationManager.AppSettings["PregancyLenth"]);
            }
            VisibleCells = new Cell[ObserveRadius * 2, ObserveRadius * 2];
            Operation = OperationChoiceHerbious.JustSwimming;
        }

        public override void Reproduction()
        {
            if (PregancyCurrent == PregancyLenght)
            {
                Cell avaibleCell = GetNonNullCellFromAvaiableCells(GetAvCells(Cell));
                FishHerbious baby = new FishHerbious
                {
                    Cell = avaibleCell
                };
                Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = baby;
            }
        }
        public void Move()
        {
            if (Operation == OperationChoiceHerbious.RunFromPredator)
            {
                Cell predatorCell = GetCellWithPredator();
                int startX = Cell.CoordX;
                int startY = Cell.CoordY;
                if (predatorCell.CoordY < Cell.CoordY)
                {

                    Cell.CoordY += MoveMaxLenght;
                    if (Cell.CoordY >= Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumHeight"]))
                    {
                        Cell.CoordY = Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumHeight"]) - 1;
                    }

                    if (Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].GetIsAvaiable() == false)
                    {
                        Cell.CoordY = GetNonNullCellFromAvaiableCells(GetAvCells(new Cell { CoordX = Cell.CoordX, CoordY = Cell.CoordY })).CoordY;
                        Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = this;
                        if (ISFishMoved(startX, startY))
                        {
                            this.Cell.SelfClean();
                        }
                        return;
                    }

                    Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = this;
                    if (ISFishMoved(startX, startY))
                    {
                        this.Cell.SelfClean();
                    }
                    return;
                }
                else if (predatorCell.CoordY > Cell.CoordY)
                {
                    Cell.CoordY -= MoveMaxLenght;
                    if (Cell.CoordY < 0)
                    {
                        Cell.CoordY = 0;
                    }
                    if (Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].GetIsAvaiable() == false)
                    {
                        Cell.CoordY = GetNonNullCellFromAvaiableCells(GetAvCells(new Cell { CoordX = Cell.CoordX, CoordY = Cell.CoordY })).CoordY;
                        if (Cell.GetIsAvaiable() == false)
                        {
                            Cell.CoordX = startX;
                            Cell.CoordY = startY;
                            return;
                        }
                        Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = this;
                        if (ISFishMoved(startX, startY))
                        {
                            this.Cell.SelfClean();
                        }
                        return;
                    }
                    Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = this;
                    if (ISFishMoved(startX, startY))
                    {
                        this.Cell.SelfClean();
                    }
                    return;
                }
                else if (predatorCell.CoordX < Cell.CoordX)
                {
                    Cell.CoordX += MoveMaxLenght;
                    if (Cell.CoordX >= Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumWidght"]))
                    {
                        Cell.CoordX = Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumWidght"]) - 1;
                    }
                    if (Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].GetIsAvaiable() == false)
                    {
                        Cell.CoordX = GetNonNullCellFromAvaiableCells(GetAvCells(new Cell { CoordX = Cell.CoordX, CoordY = Cell.CoordY })).CoordX;
                        if (Cell.GetIsAvaiable() == false)
                        {
                            Cell.CoordX = startX;
                            Cell.CoordY = startY;
                            return;
                        }
                        if (ISFishMoved(startX, startY))
                        {
                            this.Cell.SelfClean();
                        }
                        return;
                    }
                    Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = this;
                    if (ISFishMoved(startX, startY))
                    {
                        this.Cell.SelfClean();
                    }
                    return;
                }
                Cell.CoordX -= MoveMaxLenght;
                if (Cell.CoordX < 0)
                {
                    Cell.CoordX = 0;
                }
                if (Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].GetIsAvaiable() == false)
                {
                    Cell.CoordX = GetNonNullCellFromAvaiableCells(GetAvCells(new Cell { CoordX = Cell.CoordX, CoordY = Cell.CoordY })).CoordX;
                    if (Cell.GetIsAvaiable() == false)
                    {
                        Cell.CoordX = startX;
                        Cell.CoordY = startY;
                        return;
                    }
                    Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = this;
                    if (ISFishMoved(startX, startY))
                    {
                        this.Cell.SelfClean();
                    }
                    return;
                }
                Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = this;
                if (ISFishMoved(startX, startY))
                {
                    this.Cell.SelfClean();
                }

            }
            else if (Operation == OperationChoiceHerbious.MoveToFood)
            {

                Cell herbCell = GetCellWithHerb();
                if (herbCell == null)
                {
                    return;
                }
                Cell avaibleCell = GetNonNullCellFromAvaiableCells(GetAvCells(herbCell));

                int oldX = Cell.CoordX;
                int oldY = Cell.CoordY;
                Cell.CoordX = avaibleCell.CoordX;
                Cell.CoordY = avaibleCell.CoordY;
                Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = this;
                
                if (ISFishMoved(oldX, oldY))
                {
                    Aquarium.CellsArray[oldX, oldY].SelfClean();
                    Eat(herbCell.Herb);
                    return;
                    
                }
                Eat(herbCell.Herb);
                return;
            }
            else if (Operation == OperationChoiceHerbious.SearchForPartner)
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
            else if(Operation == OperationChoiceHerbious.JustSwimming)
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

        private Cell[,] GetAvCells(Cell cell)
        {
            Cell[,] avCells = new Cell[3,3];
            for (int x = cell.CoordX - 1; x < cell.CoordX + 2; x++)
            {
                for (int y = cell.CoordY - 1; y < cell.CoordY + 2; y++)
                {
                    if (x - cell.CoordX + 1 >= 3 || y - cell.CoordY + 1 >= 3 || x - cell.CoordX + 1 < 0 || y - cell.CoordY + 1 < 0)
                    {
                        continue;
                    }
                    if(y > Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumHeight"])-1 || x > Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumWidght"])-1)
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
                    else if(x < 0 || y < 0)
                    {
                        if(x < 0 && y < 0)
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
                            if (Aquarium.CellsArray[x,0] == null)
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
            if(Cell.CoordX == x && Cell.CoordY == y)
            {
                return false;
            }
            return true;
        }

        private Cell GetCellWithPredator()
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
                        if (VisibleCells[x, y].Fish.GetType().Equals(typeof(FishPredator)))
                        {
                            return VisibleCells[x, y];
                        }
                    }
                }
            }
            return null;
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
                        if (VisibleCells[x, y].Fish.GetType().Equals(typeof(FishHerbious)) && VisibleCells[x, y].Fish.CheckAbilityToReproduction(VisibleCells[x, y].Fish))
                        {
                            StartPregancy(VisibleCells[x, y].Fish);
                            return VisibleCells[x, y];
                        }
                    }
                }
            }
            return null;
        }

        private Cell GetCellWithHerb()
        {
            for (int x = 0; x < ObserveRadius * 2; x++)
            {
                for (int y = 0; y < ObserveRadius * 2; y++)
                {
                    if (VisibleCells[x, y] == null)
                    {
                        continue;
                    }
                    if (VisibleCells[x, y].Herb != null)
                    {
                        return VisibleCells[x, y];
                    }
                }
            }
            return null;
        }

        private Cell GetNonNullCellFromAvaiableCells(Cell[,] avaiableCells)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (avaiableCells[x,y] != null && avaiableCells[x, y].GetIsAvaiable())
                    {
                        return avaiableCells[x, y];
                    }
                }
            }
            return Cell;
        }

        private bool GenderRandom()
        {
            Random random = new Random();
            if (random.Next(0, 2) == 0)
            {
                return true;
            }
            return false;
        }

        public override void SetDeath()
        {
            if(AgeCurrent == AgeMax)
            {
                DeathType = Death.ByAge;
                Cell.Fish = null;
                Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = null;
            }
            else if(EnergyCurrent == 0)
            {
                DeathType = Death.ByHunger;
                Cell.Fish = null;
                Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = null;
            }
            else if (IsEaten)
            {
                DeathType = Death.ByPredator;
                Cell.Fish = null;
                Aquarium.CellsArray[Cell.CoordX, Cell.CoordY].Fish = null;
            }
        }
        public double Eat(Herb herb)
        {
            EnergyCurrent += herb.EnergyCurrent;
            herb.Cell.Herb = null;
            if (EnergyCurrent >= EnergyMax)
            {
                EnergyCurrent = EnergyMax;
            }

            return EnergyCurrent;
        }
        public void OperationAnalysis()
        {
            if (IsPredatorNear(VisibleCells))
            {
                Operation = OperationChoiceHerbious.RunFromPredator;
                return;
            }
            if (EnergyCurrent <= EnergyHungryLvl)
            {
                Operation = OperationChoiceHerbious.MoveToFood;
                return;
            }
            else if(AgeCurrent >= AgeBeginMature && AgeCurrent <= AgeEndMature && (!IsPregant))
            {
                Operation = OperationChoiceHerbious.SearchForPartner;
                return;
            }
            Operation = OperationChoiceHerbious.JustSwimming;

        }
        private bool IsPredatorNear(Cell[,] visibleCells)
        {
            for(int x = 0; x < ObserveRadius * 2; x++)
            {
                for (int y = 0; y < ObserveRadius * 2; y++)
                {
                    if(visibleCells[x, y] == null)
                    {
                        continue;
                    }
                    if(visibleCells[x, y].Fish != null)
                    {
                        if (visibleCells[x, y].Fish.GetType().Equals(typeof(FishPredator)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public override string ToString()
        {
            string fish = "H";
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

            if(AgeCurrent < AgeBeginMature)
            {
                fish += "1";
            }
            else if(AgeCurrent >= AgeBeginMature && AgeCurrent <= AgeEndMature)
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
