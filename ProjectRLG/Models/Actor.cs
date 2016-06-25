namespace ProjectRLG.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using ProjectRLG.Contracts;
    using ProjectRLG.Enums;
    using ProjectRLG.Extensions;

    public class Actor : BaseObject, IActor
    {
        private Transform _transform;

        public Actor(string name, Glyph glyph, int energy = 50)
            : this(name, 0, 0, glyph, energy, new Dictionary<string, string>())
        {
        }
        public Actor(string name, int x, int y, Glyph glyph, int energy, Dictionary<string, string> _properties)
            : base(_properties)
        {
            Name = name;
            Glyph = glyph;
            Energy = energy;
            _transform = new Transform();
            _transform.X = x;
            _transform.Y = y;
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
        public IMap CurrentMap { get; set; }

        public Point Move(CardinalDirection dir)
        {
            if (CurrentMap == null)
            {
                return this.Transform.Position;
            }

            Point newPosition = Transform.Position + dir.GetDeltaCoordinate();
            if (!CurrentMap.Cells.CellExists(newPosition.X, newPosition.Y))
            {
                return this.Transform.Position;
            }

            if (CurrentMap[newPosition].IsCellAvailable)
            {
                CurrentMap[Transform.Position].Actor = null;
                CurrentMap[newPosition].Actor = this;
                Transform.Position = newPosition;
                return this.Transform.Position;
            }

            return this.Transform.Position;
        }
    }
}
