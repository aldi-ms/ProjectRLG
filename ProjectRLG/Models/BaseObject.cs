namespace ProjectRLG.Models
{
    using System;
    using ProjectRLG.Contracts;

    public abstract class BaseObject : IBaseObject
    {
        private readonly Guid _id;

        public BaseObject()
        {
            _id = Guid.NewGuid();
        }

        public Guid Id
        {
            get { return _id; }
        }
        public virtual string Name { get; set; }
        public virtual IGlyph Glyph { get; set; }
    }
}
