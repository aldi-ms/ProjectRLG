namespace ProjectRLG.Contracts
{
    using Microsoft.Xna.Framework;

    public interface IGlyph
    {
        string Text { get; set; }
        bool IsImagePath { get; }
        Color ForegroundColor { get; set; }
        Color BackgroundColor { get; set; }
    }
}
