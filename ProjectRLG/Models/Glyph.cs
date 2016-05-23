namespace ProjectRLG.Models
{
    using ProjectRLG.Contracts;

    public struct Glyph : IGlyph
    {
        private string _text;
        private bool _isImagePath;

        public Glyph(string text, bool isImagePath = false)
        {
            _text = text;
            _isImagePath = isImagePath;
        }

        public bool IsImagePath
        {
            get
            {
                return _isImagePath;
            }
            private set
            {
                _isImagePath = value;
            }
        }
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }
    }
}
