namespace ProjectRLG.Models
{
    using Microsoft.Xna.Framework;
    using ProjectRLG.Contracts;

    public class Glyph : IGlyph
    {
        private string _text;
        private bool _isImagePath;
        private Color _foreground;
        private Color _background;

        public Glyph(string text, bool isImagePath = false)
            : this(text, Color.White, isImagePath)
        { }
        public Glyph(string text, Color foreground, bool isImagePath = false)
        {
            _text = text;
            _foreground = foreground;
            _background = Color.Black;
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
        public Color ForegroundColor
        {
            get
            {
                return _foreground;
            }
            set
            {
                _foreground = value;
            }
        }
        public Color BackgroundColor
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
            }
        }
    }
}
