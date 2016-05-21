namespace ProjectRLG.Contracts
{
    using Microsoft.Xna.Framework;

    public interface IMap : IBaseObject
    {
        int Z { get; }
        ICell this[int x, int y] { get; set; }
        ICell this[Point p] { get; set; }
        ICellCollection Cells { get; set; }
    }
}
