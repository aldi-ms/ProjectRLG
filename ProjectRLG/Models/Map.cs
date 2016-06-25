namespace ProjectRLG.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using ProjectRLG.Contracts;
    using ProjectRLG.Utilities;

    public class Map : BaseObject, IMap
    {
        private ICellCollection _mapCells;

        public Map(int width, int height)
            : this(new CellCollection(width, height))
        {
        }
        public Map(ICellCollection cells)
            : base()
        {
            _mapCells = cells;
        }

        public int Z { get; private set; }
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

        public void LoadActors(IEnumerable<IActor> actors)
        {
            foreach (IActor actor in actors)
            {
                if (actor.Transform.Equals(default(Transform)) || !this[actor.Transform.Position].IsCellAvailable)
                {
                    ICell cell = MapUtilities.GetRandomFreeCell(this);
                    cell.Actor = actor;
                }
                else
                {
                    this[actor.Transform.Position].Actor = actor;
                }

                actor.CurrentMap = this;
            }
        }
    }
}
