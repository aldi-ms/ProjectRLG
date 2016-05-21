namespace ProjectRLG.Contracts
{
    public interface ITerrain
    {
        byte Difficulty { get; set; }
        IGlyph Glyph { get; set; }
    }
}
