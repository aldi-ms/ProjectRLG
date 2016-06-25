namespace ProjectRLG.Contracts
{
    using Microsoft.Xna.Framework;
    using ProjectRLG.Enums;

    public interface IActor : IMapObject
    {
        int Energy { get; }
        IMap CurrentMap { get; set; }
        Point Move(CardinalDirection dir);
    }
}
