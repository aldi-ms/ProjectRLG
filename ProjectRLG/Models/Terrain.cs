namespace ProjectRLG.Models
{
    using ProjectRLG.Contracts;

    public class Terrain : ITerrain
    {
        public Terrain(IGlyph glyph, byte difficulty)
            : this(glyph.Text, difficulty, glyph.IsImagePath)
        {
        }
        public Terrain(string text, byte difficulty, bool isImagePath = false)
        {
            Glyph = new Glyph(text, isImagePath);
            Difficulty = difficulty;
        }

        public byte Difficulty { get; set; }
        public IGlyph Glyph { get; set; }
    }
}
