using Microsoft.Xna.Framework;
namespace ProjectRLG.Contracts
{
    public interface IPosition
    {
        int X { get; set; }
        int Y { get; set; }
        Point Position { get; set; }
    }
}
