namespace ProjectRLG.Contracts
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;
    using ProjectRLG.Infrastructure.FieldOfView;

    public interface ICell : IBaseObject, IPosition, IFovCell
    {
        ITerrain Terrain { get; set; }
        IActor Actor { get; set; }
        HashSet<IGameItem> Items { get; set; }
        HashSet<IMapObject> Objects { get; set; }
        HashSet<ISpecialObject> SpecialObjects { get; set; }

        bool IsCellAvailable { get; }
    }
}
