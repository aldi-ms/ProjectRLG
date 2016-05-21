namespace ProjectRLG.Contracts
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using ProjectRLG.Infrastructure.FieldOfView;

    public interface ICell : IBaseObject, IPosition, IFovCell, IPropertyBag
    {
        ITerrain Terrain { get; set; }
        IActor Actor { get; set; }
        ICell Empty { get; }
        HashSet<IGameItem> Items { get; set; }
        HashSet<IMapObject> Objects { get; set; }
        HashSet<ISpecialObject> SpecialObjects { get; set; }

        T GetAll<T>();
        T Get<T>(object obj);
    }
}
