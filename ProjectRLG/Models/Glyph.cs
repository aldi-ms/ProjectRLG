namespace ProjectRLG.Models
{
    using ProjectRLG.Contracts;

    public class Glyph : IGlyph
    {
        private bool _isImagePath;

        public Glyph()
        {
        }
        public Glyph(string text, bool isImagePath = false)
        {
            Text = text;
            _isImagePath = isImagePath;
        }

        public bool IsImagePath { get; private set; }
        public string Text { get; set; }
    }
}
