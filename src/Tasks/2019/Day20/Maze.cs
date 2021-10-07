using System.Collections.Generic;

namespace App.Tasks.Year2019.Day20
{
    public class Maze
    {
        public int CountStepsNeededToGetFromStartTileToEndTile(
            Dictionary<string, PortalPair> portalPairs,
            MazeElement[,] mazeMap
        )
        {
            return mazeMap.Length;
        }
    }
}
