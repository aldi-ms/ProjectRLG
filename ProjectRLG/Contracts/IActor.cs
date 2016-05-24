namespace ProjectRLG.Contracts
{
    using ProjectRLG.Enums;

    public interface IActor : IMapObject
    {
        int Energy { get; }
    }
}
