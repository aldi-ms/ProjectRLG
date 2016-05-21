namespace ProjectRLG.Models
{
    using System;
    using System.Collections.Generic;
    using ProjectRLG.Contracts;

    public class Cell : ICell
    {
        private IGlyph _cellGlyph;

        public Cell()
        {
            Items = new HashSet<IGameItem>();
            Objects = new HashSet<IMapObject>();
            SpecialObjects = new HashSet<ISpecialObject>();
        }

        public bool IsTransparent
        {
            get { return !Glyph.Text.Equals("#"); }
        }
        public bool IsVisible { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public ITerrain Terrain { get; set; }
        public IActor Actor { get; set; }
        public ICell Empty { get; set; }
        public HashSet<IGameItem> Items { get; set; }
        public HashSet<IMapObject> Objects { get; set; }
        public HashSet<ISpecialObject> SpecialObjects { get; set; }
        public Guid Id { get; set; }
        public IGlyph Glyph
        {
            get
            {
                if (Actor != null)
                {
                    _cellGlyph = Actor.Glyph;
                }
                if (Terrain != null)
                {
                    _cellGlyph = Terrain.Glyph;
                }
                if (_cellGlyph == null)
                {
                    _cellGlyph = new Glyph("er");
                }

                return _cellGlyph;
            }
            set
            {
                this._cellGlyph = value;
            }
        }

        public T GetAll<T>()
        {
            throw new System.NotImplementedException();
        }
        public T Get<T>(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
