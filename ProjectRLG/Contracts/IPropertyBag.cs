namespace ProjectRLG.Contracts
{
    using System.Collections.Generic;

    public interface IPropertyBag
    {
        Dictionary<string, object> PropertyBag { get; set; }
    }
}
