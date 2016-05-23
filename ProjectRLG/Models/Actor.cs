namespace ProjectRLG.Models
{
    using System;
    using ProjectRLG.Contracts;

    public class Actor : BaseObject, IActor
    {
        private Transform _transform;

        public Actor(string name, int x, int y, Glyph glyph, int energy = 50)
            : this(name, glyph, energy)
        {
            _transform.X = x;
            _transform.Y = y;
        }
        public Actor(string name, Glyph glyph, int energy = 50) 
            : base()
        {
            Name = name;
            Glyph = glyph;
            Energy = energy;
            _transform.Facing = Enums.CardinalDirection.South;
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
