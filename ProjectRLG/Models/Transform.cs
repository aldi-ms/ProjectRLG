namespace ProjectRLG.Models
{
    using ProjectRLG.Contracts;
    using ProjectRLG.Enums;

    public class Transform : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CardinalDirection Facing { get; set; }
    }
}
