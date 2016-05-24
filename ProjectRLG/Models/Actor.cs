namespace ProjectRLG.Models
{
    using System;
    using ProjectRLG.Contracts;
using System.Collections.Generic;

    public class Actor : BaseObject, IActor
    {
        private Transform _transform;
        private Dictionary<string, string> _propertyBag;

        public Actor(string name, Glyph glyph, int energy = 50)
            : this(name, 0, 0, glyph, energy, new Dictionary<string,string>())
        {
        }
        public Actor(string name, int x, int y, Glyph glyph, int energy, Dictionary<string, string> _properties)
            : base(_properties)
        {
            Name = name;
            Glyph = glyph;
            Energy = energy;
            _transform.X = x;
            _transform.Y = y;
            _transform.Facing = Enums.CardinalDirection.South;
            _propertyBag = new Dictionary<string, string>();
        }

        public int Energy { get; private set; }
        public Transform Transform 
        {
            get
            {
                return _transform;
            }
            set
            {
                _transform = value;
            }
        }
    }
}
