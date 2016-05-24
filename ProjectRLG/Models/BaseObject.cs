namespace ProjectRLG.Models
{
    using System;
    using System.Collections.Generic;
    using ProjectRLG.Contracts;

    public abstract class BaseObject : IBaseObject
    {
        private readonly Guid _id;

        public BaseObject() : this(new Dictionary<string, string>())
        {
        }
        public BaseObject(Dictionary<string, string> properties)
        {
            if (properties == null)
            {
                properties = new Dictionary<string, string>();
            }

            Properties = new PropertyBag(properties);
            _id = Guid.NewGuid();
        }

        public PropertyBag Properties { get; set; }
        public Guid Id
        {
            get { return _id; }
        }
        public virtual string Name { get; set; }
        public virtual IGlyph Glyph { get; set; }
    }
}
