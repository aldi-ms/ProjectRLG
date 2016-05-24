namespace ProjectRLG.Utilities
{
    using System;
    using ProjectRLG.Contracts;
    using ProjectRLG.Infrastructure;
    using ProjectRLG.Models;
    using Microsoft.Xna.Framework;

    public static class MapUtilities
    {
        private static Random _rng;
        private static IGlyph _wallGlyph;
        private static IGlyph _grassGlyph;

        static MapUtilities()
        {
            _rng = new Random();
            _wallGlyph = new Glyph("#");
            _grassGlyph = new Glyph(",");
        }

        public static ICellCollection CreateRandomCellCollection(int width, int height)
        {
            ICell[][] cellMatrix = new Cell[width][];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cellMatrix[i] = new Cell[height];
                }
            }

            byte difficulty;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    IGlyph randomGlyph = _rng.Next(0, 4) == 0 ? _wallGlyph : _grassGlyph;
                    difficulty = randomGlyph.Text.Equals("#") ? (byte)100 : (byte)5;
                    cellMatrix[i][j] = new Cell()
                    {
                        X = i,
                        Y = j,
                        Name = string.Format("cell[{0}, {1}]", i, j),
                        Terrain = new Terrain(randomGlyph, difficulty)
                    };
                }
            }

            return new CellCollection(cellMatrix);
        }

        public static ICell GetRandomFreeCell(IMap map)
        {
            Point cellCoords;
            do
            {
                cellCoords = new Point(
                    _rng.Next(0, map.Cells.Width),
                    _rng.Next(0, map.Cells.Height));
            } while (!map[cellCoords].IsCellAvailable);
            
            return map[cellCoords];
        }
    }
}
