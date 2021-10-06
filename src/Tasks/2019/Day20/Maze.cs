using System.Collections.Generic;

namespace App.Tasks.Year2019.Day20
{
    public class Maze
    {
        private const char EMPTY_SPACE = ' ';

        private const char OPEN_PASSAGE = '.';

        private const char SOLID_WALL = '#';

        public int CountStepsNeededToGetFromStartTileToEndTile(
            Dictionary<string, PortalPair> portalPairs,
            char[,] mazeMap
        )
        {
            return mazeMap.Length;
        }
    }
}
