namespace ProjectRLG.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProjectRLG.Contracts;

    public class CellCollection : ICellCollection
    {
        private ICell[][] data;

        public CellCollection(int width, int height)
        {
            this.Initialize(width, height);
        }
        
        public int Count
        {
            get 
            {
                List<ICell> result = this.data.SelectMany(x => x, (y, el) => el).ToList();
                return result.Count;
            }
        }
        public int Capacity
        {
            get
            {
                return this.Width * this.Height;
            }
        }
        public int Width
        {
            get
            {
                return data.Length;
            }
        }
        public int Height
        {
            get
            {
                return data[0].Length;
            }
        }
        public ICell this[int x, int y]
        {
            get
            {
                return data[x][y];
            }

            set
            {
                data[x][y] = value;
            }
        }

        public void Clear()
        {
            data = null;
            Initialize();
        }
        public bool Contains(ICell item)
        {
            for (int x = 0; x < this.data.GetLength(0); x++)
            {
                for (int y = 0; y < this.data.GetLength(1); y++)
                {
                    if (this.data[x][y].Id == item.Id)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public int Add(ICell cell)
        {
            if (cell != null)
            {
                this.data[cell.X][cell.Y] = cell;
                return 1;
            }
            return -1;
        }
        public void Insert(int x, int y, ICell cell)
        {
            this.data[x][y] = cell;
        }
        public void Remove(ICell cell)
        {
            this.data[cell.X][cell.Y] = null;
        }
        public void RemoveAt(int x, int y)
        {
            this.data[x][y] = null;
        }

        private void Initialize(int x, int y)
        {
            if (x > 0 && y > 0)
            {
                this.data = new Cell[x][];
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < this.Height; j++)
                    {
                        this.data[i] = new Cell[j];
                    }
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("X and Y should be > 0. Values passed: [{0}], [{1}].", x, y));
            }
        }
    }
}
