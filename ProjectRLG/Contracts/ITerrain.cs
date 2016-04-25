namespace ProjectRLG.Contracts
{
    public interface ITerrain
    {
        int Difficulty { get; set; }
        IGlyph Glyph { get; set; }
    }
}
