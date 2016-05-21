namespace ProjectRLG.Models
{
    using System;
    using ProjectRLG.Contracts;
using Microsoft.Xna.Framework;

    public class Map : IMap
    {
        private ICellCollection _mapCells;

        public Map(int width, int height)
        {
            _mapCells = new CellCollection(width, height);
        }
        public Map(ICellCollection cells)
        {
            _mapCells = cells;
        }

        public ICell this[int x, int y] 
        {
            get 
            { 
                return _mapCells[x, y]; 
            }
            set 
            {
                _mapCells[x, y] = value; 
            }
        }
        public ICell this[Point p]
        {
            get
            {
                return _mapCells[p.X, p.Y];
            }
            set
            {
                _mapCells[p.X, p.Y] = value;
            }
        }
        public int Z { get; private set; }
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public ICellCollection Cells
        {
            get
            {
                return _mapCells;
            }
            set
            {
                _mapCells = value;
            }
        }
        public IGlyph Glyph { get; set; }
    }
}
