namespace ProjectRLG.Contracts
{
    using System;

    public interface IBaseObject
    {
        Guid Id { get; }
        string Name { get; set; }
        IGlyph Glyph { get; set; }
    }
}
