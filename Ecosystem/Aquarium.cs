using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Linq;

namespace Ecosystem
{
    public  class Aquarium
    {
        private int _cellsQuantity;
        private int _widht;
        private int _height;
        private int _stonesQuantity;
        private int _herbsQuantity;
        private int _fishHerbQuantity;
        private int _fishPredatorQuantity;
        private List<Stone> _stonesArray;
        private List<Herb> _herbsArray;
        private List<FishHerbious> _fishHerbivorousArray;
        private List<FishPredator> _fishPredatorArray;
        private static Cell[,] _cellsArray;
        public static Cell[,] CellsArray
        {
            get
            {
                return _cellsArray;
            }
            set
            {
                _cellsArray = value;
            }
        }

        public List<Stone> StonesArray
        {
            get
            {
                return _stonesArray;
            }
            set
            {
                _stonesArray = value;
            }
        }
        public int StonesQuantity
        {
            get
            {
                return _stonesQuantity;
            }
            set
            {
                _stonesQuantity = value;
            }
        }
        public int CellsQuantity
        {
            get
            {
                return _cellsQuantity;
            }
            set
            {
                _cellsQuantity = value;
            }
        }
        public int HerbsQuantity
        {
            get
            {
                return _herbsQuantity;
            }
            set
            {
                _herbsQuantity = value;
            }

        }
        public int FishHerbQuantity
        {
            get
            {
                return _fishHerbQuantity;
            }
            set
            {
                _fishHerbQuantity = value;
            }

        }
        public int FishPredatorQuantity
        {
            get
            {
                return _fishPredatorQuantity;
            }
            set
            {
                _fishPredatorQuantity = value;
            }

        }
        public List<Herb> HerbsArray
        {
            get
            {
                return _herbsArray;
            }
            set
            {
                _herbsArray = value;
            }

        }
        public List<FishHerbious> FishHerbivorousArray
        {
            get
            {
                return _fishHerbivorousArray;
            }
            set
            {
                _fishHerbivorousArray = value;
            }

        }
        public List<FishPredator> FishPredatorArray
        {
            get
            {
                return _fishPredatorArray;
            }
            set
            {
                _fishPredatorArray = value;
            }

        }
        public Aquarium()
        {
            _height = Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumHeight"]);
            _widht = Convert.ToInt32(ConfigurationManager.AppSettings["AuqariumWidght"]);
            _cellsQuantity = _widht * _height;
            _cellsArray = new Cell[_widht, _height];
        }
        public void StartEcosystem()
        {
            GiveCoordsToCells();
            StonesArray = CreatingStones();
            HerbsArray =  CreatingHerbs();
            FishHerbivorousArray = CreatingHerbvious();
            FishPredatorArray = CreatingPredators();
            SetStonesAtCells();
            SetHerbsAtCells();
            SetFishHerbiousAtCells();
            SetFishPredatorsAtCells();
            ShowCurrentElements();
        }
        public void NextIteration()
        {
            Console.WriteLine();
            Console.WriteLine();
            UpdateFishStatus();
            ShowCurrentElements();
        }
        private void UpdateFishStatus()
        {
            foreach(var fish in FishHerbivorousArray)
            {
                fish.SetDeath();
                fish.AgeCurrent += 1;
                fish.EnergyCurrent -= fish.EnergyIterationDecrease;
                SetVisibleCellsForFish(fish, fish.ObserveRadius);
                fish.OperationAnalysis();
                fish.Move();
                if(fish.IsMale == false)
                {
                    fish.Reproduction();

                }
            }
            foreach(var fish in FishPredatorArray)
            {
                fish.AgeCurrent += 1;
                fish.EnergyCurrent -= fish.EnergyIterationDecrease;
                SetVisibleCellsForFish(fish, fish.ObserveRadius);
                fish.OperationAnalysis();
                fish.Move();
                if (fish.IsMale == false)
                {
                    fish.Reproduction();

                }
            }
        }
        private List<Stone> CreatingStones()
        {
            List<Stone> stones = new List<Stone>();
            for (int i = 0; i < _stonesQuantity; i++)
            {
                stones.Add(new Stone());
            }
            return stones;
        }
        private List<FishPredator> CreatingPredators()
        {
            List<FishPredator> fishPredators = new List<FishPredator>();
            for (int i = 0; i < _fishPredatorQuantity; i++)
            {
                fishPredators.Add(new FishPredator());
            }
            return fishPredators;
        }
        public void SetVisibleCellsForFish(Fish fish, int obsRad)
        {
            int startXCoord = fish.Cell.CoordX - obsRad;
            int startYCoord = fish.Cell.CoordY - obsRad;
            if (startXCoord < 0)
            {
                startXCoord = 0;
            }
            if (startYCoord < 0)
            {
                startYCoord = 0;
            }
            for (int y = startYCoord; y < obsRad * 2; y++)
            {
                for (int x = startXCoord; x < obsRad * 2; x++)
                {
                    if(x >= _height || y >= _widht)
                    {
                        continue;
                    }
                    fish.VisibleCells[x - startXCoord, y - startYCoord] = _cellsArray[x, y];
                }
            }
        }
        private List<FishHerbious> CreatingHerbvious()
        {
            List<FishHerbious> fishHerbivorous = new List<FishHerbious>();
            for (int i = 0; i < _fishHerbQuantity; i++)
            {
                fishHerbivorous.Add(new FishHerbious());
            }
            return fishHerbivorous;
        }
        private List<Herb> CreatingHerbs()
        {
            List<Herb> herbs = new List<Herb>();
            for (int i = 0; i < _herbsQuantity; i++)
            {
                herbs.Add(new Herb());
            }
            return herbs;
        }
        private void GiveCoordsToCells()
        {
            for(int x = 0; x < _widht; x++)
            {
                for(int y = 0; y < _height; y++)
                {
                    _cellsArray[x, y] = new Cell
                    {
                        CoordX = x,
                        CoordY = y
                    };
                }
            }
        }
        private void ShowCurrentElements()
        {
            
            for (int x = 0; x < _widht; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    //Console.Write($"{x},{y} \t");
                    Console.Write($"{_cellsArray[x, y]} \t");


                }
                Console.WriteLine();
            }
            Console.WriteLine($"Stones: {StoneCounter()}");
            Console.WriteLine($"Herbs: {HerbCounter()}");
            Console.WriteLine($"FishHerb: {FishHerbCounter()}");
            Console.WriteLine($"FishPredator: {FishPredatorCounter()}");
        }

        private int StoneCounter()
        {
            int counter = 0;
            for (int x = 0; x < _widht; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if(_cellsArray[x, y].GetContentByCoords(x, y) != null)
                    {
                        if (_cellsArray[x, y].GetContentByCoords(x, y).GetType() == typeof(Stone))
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }
        private int HerbCounter()
        {
            int counter = 0;
            for (int x = 0; x < _widht; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (_cellsArray[x, y].GetContentByCoords(x, y) != null)
                    {
                        if (_cellsArray[x, y].GetContentByCoords(x, y).GetType() == typeof(Herb))
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }
        private int FishHerbCounter()
        {
            int counter = 0;
            for (int x = 0; x < _widht; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (_cellsArray[x, y].GetContentByCoords(x, y) != null)
                    {
                        if (_cellsArray[x, y].GetContentByCoords(x, y).GetType() == typeof(FishHerbious))
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }
        private int FishPredatorCounter()
        {
            int counter = 0;
            for (int x = 0; x < _widht; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (_cellsArray[x, y].GetContentByCoords(x, y) != null)
                    {
                        if (_cellsArray[x, y].GetContentByCoords(x, y).GetType() == typeof(FishPredator))
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }
        private void SetStonesAtCells()
        {
            Random random = new Random();
            bool flag;
            foreach (var stone in _stonesArray)
            {
                flag = true;
                while (flag)
                {
                    int randomX = random.Next(0, _widht);
                    int randomY = random.Next(0, _height);
                    if (_cellsArray[randomX, randomY].GetIsAvaiable())
                    {
                        stone.Cell = _cellsArray[randomX, randomY];
                        _cellsArray[randomX, randomY].Stone = stone;
                        _cellsArray[randomX, randomY].Stone.Cell = _cellsArray[randomX, randomY];
                        flag = false;
                    }
                }
            }
        }
        private void SetHerbsAtCells()
        {
            Random random = new Random();
            bool flag;
            foreach (var herb in _herbsArray)
            {
                flag = true;
                while (flag)
                {
                    int randomX = random.Next(0, _widht);
                    int randomY = random.Next(0, _height);
                    if (_cellsArray[randomX, randomY].GetIsAvaiable())
                    {
                        herb.Cell = _cellsArray[randomX, randomY];
                        _cellsArray[randomX, randomY].Herb = herb;
                        _cellsArray[randomX, randomY].Herb.Cell = _cellsArray[randomX, randomY];
                        flag = false;
                    }
                }
            }
        }
        private void SetFishPredatorsAtCells()
        {
            Random random = new Random();
            bool flag;
            foreach (var fish in _fishPredatorArray)
            {
                flag = true;
                while (flag)
                {
                    int randomX = random.Next(0, _widht);
                    int randomY = random.Next(0, _height);
                    if (_cellsArray[randomX, randomY].GetIsAvaiable())
                    {
                        fish.Cell = _cellsArray[randomX, randomY];
                        _cellsArray[randomX, randomY].Fish = fish;
                        _cellsArray[randomX, randomY].Fish.Cell = _cellsArray[randomX, randomY];
                        flag = false;
                    }
                }
            }
        }
        private void SetFishHerbiousAtCells()
        {
            Random random = new Random();
            bool flag;
            foreach (var fish in _fishHerbivorousArray)
            {
                flag = true;
                while (flag)
                {
                    int randomX = random.Next(0, _widht);
                    int randomY = random.Next(0, _height);
                    if (_cellsArray[randomX, randomY].GetIsAvaiable())
                    {
                        fish.Cell = _cellsArray[randomX, randomY];
                        _cellsArray[randomX, randomY].Fish = fish;
                        _cellsArray[randomX, randomY].Fish.Cell = _cellsArray[randomX, randomY];
                        flag = false;
                    }
                }
            }
        }
    }
}
