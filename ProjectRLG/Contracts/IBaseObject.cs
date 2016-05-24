namespace ProjectRLG.Contracts
{
    using ProjectRLG.Models;
    using System;

    public interface IBaseObject
    {
        Guid Id { get; }
        string Name { get; set; }
        IGlyph Glyph { get; set; }
        PropertyBag Properties { get; set; }
    }
}
