using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day20
{
    public class TilesRepository
    {
        public Dictionary<int, string[]> GetTiles(string input)
        {
            Dictionary<int, string[]> tiles = new Dictionary<int, string[]>();

            string[] tilesStrings = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries
            );

            Regex tileIdRegex = new Regex(@"^Tile\s(\d+):$");

            foreach (string tileString in tilesStrings)
            {
                string[] tileParts = tileString.Split(Environment.NewLine);

                Match match = tileIdRegex.Match(tileParts[0]);
                GroupCollection groups = match.Groups;

                int tileId = int.Parse(groups[1].Value);

                tiles.Add(tileId, tileParts.Skip(1).ToArray());
            }

            return tiles;
        }
    }
}
