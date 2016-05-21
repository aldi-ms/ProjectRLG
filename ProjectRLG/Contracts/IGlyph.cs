namespace ProjectRLG.Contracts
{
    public interface IGlyph
    {
        string Text { get; set; }
        bool IsImagePath { get; }
    }
}
