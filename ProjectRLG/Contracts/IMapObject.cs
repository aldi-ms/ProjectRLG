using ProjectRLG.Models;
namespace ProjectRLG.Contracts
{
    public interface IMapObject : IBaseObject
    {
        Transform Transform { get; set; }
    }
}
