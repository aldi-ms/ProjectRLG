namespace ProjectRLG.Models
{
    using Microsoft.Xna.Framework;
    using ProjectRLG.Contracts;
    using ProjectRLG.Enums;

    public class Transform : IPosition
    {
        private Point _p;

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
        public CardinalDirection Facing { get; set; }
        public Point Position
        {
            get
            {
                return _p;
            }
            set
            {
                _p = value;
            }
        }
    }
}
