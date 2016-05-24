namespace ProjectRLG.Models
{
    using System.Collections.Generic;
    using ProjectRLG.Contracts;
    using System;

    public class PropertyBag : IPropertyBag
    {
        private Dictionary<string, string> _propertyBag;

        public PropertyBag()
        {
            _propertyBag = new Dictionary<string, string>();
        }
        public PropertyBag(Dictionary<string, string> properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException();
            }

            _propertyBag = new Dictionary<string, string>(properties);
        }

        public string this[string key]
        {
            get
            {
                if (_propertyBag.ContainsKey(key))
                {
                    return _propertyBag[key];
                }

                return null;
            }
            set
            {
                if (_propertyBag.ContainsKey(key))
                {
                    _propertyBag[key] = value;
                }
                else
                {
                    _propertyBag.Add(key, value);
                }
            }
        }

        public string GetProperty(string key)
        {
            return this[key];
        }
        public void SetProperty(string key, string value)
        {
            this[key] = value;
        }
        public bool PropertyExistsAndIsNotNull(string key)
        {
            return !string.IsNullOrEmpty(this[key]);
        }
    }
}
