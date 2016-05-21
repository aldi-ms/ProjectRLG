namespace ProjectRLG.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProjectRLG.Contracts;

    public class CellCollection : ICellCollection
    {
        private ICell[][] _data;

        public CellCollection(int width, int height)
        {
            width = Math.Abs(width);
            height = Math.Abs(height);
            _data = CreateEmptyICellMatrix(width, height);
        }
        public CellCollection(ICell[][] cells)
        {
            _data = cells;
        }

        public int Count
        {
            get
            {
                List<ICell> result = this._data.SelectMany(x => x, (y, el) => el).ToList();
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
                return _data.Length;
            }
        }
        public int Height
        {
            get
            {
                return _data[0].Length;
            }
        }
        public ICell this[int x, int y]
        {
            get
            {
                return _data[x][y];
            }

            set
            {
                _data[x][y] = value;
            }
        }

        public void Clear()
        {
            _data = CreateEmptyICellMatrix(Width, Height);
        }
        public bool Contains(ICell item)
        {
            for (int x = 0; x < this._data.GetLength(0); x++)
            {
                for (int y = 0; y < this._data.GetLength(1); y++)
                {
                    if (this._data[x][y].Id == item.Id)
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
                this._data[cell.X][cell.Y] = cell;
                return 1;
            }
            return -1;
        }
        public void Insert(int x, int y, ICell cell)
        {
            this._data[x][y] = cell;
            cell.X = x;
            cell.Y = y;
        }
        public void Remove(ICell cell)
        {
            this._data[cell.X][cell.Y] = null;
        }
        public void RemoveAt(int x, int y)
        {
            this._data[x][y] = null;
        }

        private static ICell[][] CreateEmptyICellMatrix(int width, int height)
        {
            ICell[][] resultMatrix = new Cell[width][];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    resultMatrix[i] = new Cell[height];
                }
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    resultMatrix[i][j] = new Cell();
                }
            }

            return resultMatrix;
        }
    }
}
