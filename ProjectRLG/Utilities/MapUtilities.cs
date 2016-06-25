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
            _grassGlyph = new Glyph(",", Color.Green);
        }

        public static ICellCollection CreateRandomCellCollection(int x, int y)
        {
            ICell[][] cellMatrix = new Cell[x][];
            for (int i = 0; i < x; i++)
            {
                cellMatrix[i] = new Cell[y];                
            }

            byte difficulty;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    IGlyph randomGlyph = _rng.Next(0, 4) == 0 ? _wallGlyph : _grassGlyph;
                    difficulty = randomGlyph.Text.Equals("#") ? (byte)100 : (byte)5;
                    ITerrain terrain = new Terrain(
                        new Glyph(randomGlyph.Text, randomGlyph.ForegroundColor),
                        difficulty);

                    cellMatrix[i][j] = new Cell()
                    {
                        Position = new Point(i, j),
                        Name = string.Format("cell[{0}, {1}]", i, j),
                        Terrain = terrain
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
                    _rng.Next(0, map.Cells.X),
                    _rng.Next(0, map.Cells.Y));
            } while (!map[cellCoords].IsCellAvailable);
            
            return map[cellCoords];
        }
    }
}
