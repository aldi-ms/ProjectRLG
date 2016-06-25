namespace ProjectRLG.Contracts
{
    using ProjectRLG.Infrastructure.FieldOfView;

    public interface IFovCellCollection<TFovCell>
        where TFovCell : IFovCell
    {
        int X { get; }
        int Y { get; }
        ICell this[int x, int y] { get; set; }
    }
}
