using ProjectRLG.Enums;
namespace ProjectRLG.Contracts
{
    public interface IActor : IMapObject
    {
        int Energy { get; }
        ActorRank Rank { get; }
    }
}
