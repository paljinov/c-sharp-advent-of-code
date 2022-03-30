using System.Collections.Generic;

namespace App.Tasks.Year2021.Day23
{
    public class Amphipods
    {
        private readonly Dictionary<char, int> amphipodsEnergy = new Dictionary<char, int>()
        {
            { 'A', 1},
            { 'B', 10},
            { 'C', 100},
            { 'D', 1000}
        };

        private readonly int hallwayHorizontalPosition = 1;

        private readonly Dictionary<char, int> roomsVerticalPosition = new Dictionary<char, int>()
        {
            { 'A', 2},
            { 'B', 4},
            { 'C', 6},
            { 'D', 8}
        };

        public int CalculateLeastEnergyRequiredToOrganizeTheAmphipods(char[,] amphipodsBurrow)
        {
            return amphipodsBurrow.Length;
        }

        public int CalculateLeastEnergyRequiredToOrganizeTheAmphipodsForFullDiagram(char[,] amphipodsBurrow)
        {
            return amphipodsBurrow.Length;
        }
    }
}
