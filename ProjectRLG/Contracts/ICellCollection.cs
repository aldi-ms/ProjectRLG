namespace ProjectRLG.Contracts
{
    using System.Collections.Generic;

    public interface ICellCollection
    {
        int Count { get; }
        int Capacity { get; }
        int Width { get; }
        int Height { get; }
        bool IsFixedSize { get; }
        bool IsReadOnly { get; }
        ICell this[int x, int y] { get; set; }
        int Add(ICell cell);
        void Clear();
        bool Contains(ICell cell);
        int IndexOf(ICell cell);
        void Insert(int x, int y, ICell cell);
        void Remove(ICell cell);
        void RemoveAt(int x, int y);
    }
}
