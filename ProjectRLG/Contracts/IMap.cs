namespace ProjectRLG.Contracts
{
    public interface IMap : IBaseObject
    {
        int Z { get; }
        ICellCollection Cells { get; set; }
    }
}
