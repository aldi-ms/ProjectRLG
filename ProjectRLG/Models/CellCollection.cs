namespace ProjectRLG.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProjectRLG.Contracts;
    using ProjectRLG.Infrastructure;

    public class CellCollection : ICellCollection
    {
        private ICell[][] _data;

        public CellCollection(int x, int y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
            _data = CreateEmptyICellMatrix(x, y);
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
                return this.Y * this.X;
            }
        }
        public int Y
        {
            get
            {
                return _data[0].Length;
            }
        }
        public int X
        {
            get
            {
                return _data.Length;
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
            _data = CreateEmptyICellMatrix(Y, X);
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
        public bool CellExists(int x, int y)
        {
            return !(x < 0 || x >= X || y < 0 || y >= Y);
        }

        private static ICell[][] CreateEmptyICellMatrix(int x, int y)
        {
            ICell[][] resultMatrix = new Cell[x][];
            for (int i = 0; i < x; i++)
            {
                resultMatrix[i] = new Cell[y];
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    resultMatrix[i][j] = new Cell();
                }
            }

            return resultMatrix;
        }
    }
}
