namespace ProjectRLG.Contracts
{
    using ProjectRLG.Infrastructure.FieldOfView;

    public interface IFovCellCollection<TFovCell>
        where TFovCell : IFovCell
    {
        int Height { get; }
        int Width { get; }
        ICell this[int x, int y] { get; set; }
    }
}
