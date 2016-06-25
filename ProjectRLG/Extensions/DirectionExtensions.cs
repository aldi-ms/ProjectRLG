namespace ProjectRLG.Extensions
{
    using Microsoft.Xna.Framework;
    using ProjectRLG.Enums;

    public static class DirectionExtensions
    {
        public static Point GetDeltaCoordinate(this CardinalDirection direction)
        {
            int dX = 0;
            int dY = 0;

            switch (direction)
            {
                case CardinalDirection.North:
                    dX = -1;
                    break;

                case CardinalDirection.South:
                    dX = 1;
                    break;

                case CardinalDirection.West:
                    dY = -1;
                    break;

                case CardinalDirection.East:
                    dY = 1;
                    break;

                case CardinalDirection.NorthWest:
                    dX = -1;
                    dY = -1;
                    break;

                case CardinalDirection.NorthEast:
                    dX = -1;
                    dY = 1;
                    break;

                case CardinalDirection.SouthEast:
                    dX = 1;
                    dY = 1;
                    break;

                case CardinalDirection.SouthWest:
                    dX = 1;
                    dY = -1;
                    break;
            }

            return new Point(dX, dY);
        }
    }
}
