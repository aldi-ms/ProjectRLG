namespace ProjectRLG.Models
{
    using System;
    using System.Collections.Generic;
    using ProjectRLG.Contracts;
using Microsoft.Xna.Framework;

    public class Cell : BaseObject, ICell
    {
        private Point _p;
        private ITerrain _terrain;
        private Dictionary<string, string> _propertyBag;

        public Cell() : base()
        {
            _propertyBag = new Dictionary<string, string>();
            Items = new HashSet<IGameItem>();
            Objects = new HashSet<IMapObject>();
            SpecialObjects = new HashSet<ISpecialObject>();
        }

        public string this[string key]
        {
            get
            {
                if (_propertyBag.ContainsKey(key))
                {
                    return _propertyBag[key];
                }

                return null;
            }
            set
            {
                if (_propertyBag.ContainsKey(key))
                {
                    _propertyBag[key] = value;
                }
                else
                {
                    _propertyBag.Add(key, value);
                }
            }
        }
        public bool IsTransparent
        {
            get { return !Glyph.Text.Equals("#"); }
        }
        public bool IsVisible { get; set; }
        public int X 
        {
            get
            {
                return _p.X;
            }
            set
            {
                _p.X = value;
            }
        }
        public int Y 
        {
            get
            {
                return _p.Y;
            }
            set
            {
                _p.Y = value;
            }
        }
        public ITerrain Terrain
        {
            get
            {
                if (_terrain == null)
                {
                    _terrain = new Terrain(new Glyph(" "), 0);
                }

                return _terrain;
            }
            set
            {
                _terrain = value;
            }
        }
        public IActor Actor { get; set; }
        public HashSet<IGameItem> Items { get; set; }
        public HashSet<IMapObject> Objects { get; set; }
        public HashSet<ISpecialObject> SpecialObjects { get; set; }
        public Point Point
        {
            get
            {
                return _p;
            }
        }

        public string GetProperty(string key)
        {
            return this[key];
        }
        public bool GetPropertyAsBool(string key)
        {
            return !string.IsNullOrEmpty(this[key]);
        }
        public void SetProperty(string key, string value)
        {
            this[key] = value;
        }
        public override IGlyph Glyph
        {
            get
            {
                if (Actor != null)
                {
                    return Actor.Glyph;
                }
                else if (Terrain != null)
                {
                    return Terrain.Glyph;
                }

                return new Glyph(" ");
            }
            set
            {
            }
        }
        public bool IsCellAvailable()
        {
            return (Actor == null && Terrain.Difficulty < 50);
        }
    }
}
