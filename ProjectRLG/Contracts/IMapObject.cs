namespace ProjectRLG.Contracts
{
    using ProjectRLG.Models;

    public interface IMapObject : IBaseObject
    {
        Transform Transform { get; set; }
    }
}
