namespace ProjectRLG.Contracts
{
    using ProjectRLG.Enums;

    public interface IActor : IMapObject, IPropertyBag
    {
        int Energy { get; }
    }
}
