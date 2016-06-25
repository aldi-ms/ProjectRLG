namespace ProjectRLG.Contracts
{
    using System.Collections.Generic;
    using ProjectRLG.Infrastructure.FieldOfView;

    public interface ICellCollection : IFovCellCollection<ICell>
    {
        int Count { get; }
        int Capacity { get; }
        int Add(ICell cell);
        void Clear();
        bool Contains(ICell cell);
        void Insert(int x, int y, ICell cell);
        void Remove(ICell cell);
        void RemoveAt(int x, int y);
        bool CellExists(int x, int y);
    }
}
