using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day16
{
    public class Dance
    {
        private readonly int totalPrograms = 16;

        public string GetProgramsOrderAfterDance(List<IDanceMove> danceMoves, int totalDances = 1)
        {
            char[] programs = InitializePrograms();
            Dictionary<string, int> repetitions = new Dictionary<string, int>();

            for (int i = 0; i < totalDances; i++)
            {
                programs = DoSingleDance(programs, danceMoves);
                string programsString = new string(programs);

                // If result repeated circle is found
                if (repetitions.ContainsKey(programsString))
                {
                    int indexAfterTotalDances = (totalDances - 1) % i;
                    string programsAfterTotalDances = repetitions.First(r => r.Value == indexAfterTotalDances).Key;

                    return programsAfterTotalDances;
                }

                repetitions.Add(programsString, i);
            }

            return new string(programs);
        }

        private char[] DoSingleDance(char[] programs, List<IDanceMove> danceMoves)
        {
            foreach (IDanceMove danceMove in danceMoves)
            {
                switch (danceMove.DanceMoveType)
                {
                    case DanceMoveType.Spin:
                        DanceMove<int> danceMoveSpin = (DanceMove<int>)danceMove;
                        programs = Spin(programs, danceMoveSpin.ValueA);
                        break;
                    case DanceMoveType.Exchange:
                        DanceMove<int> danceMoveExchange = (DanceMove<int>)danceMove;
                        programs = Exchange(programs, danceMoveExchange.ValueA, danceMoveExchange.ValueB);
                        break;
                    case DanceMoveType.Partner:
                        DanceMove<char> danceMovePartner = (DanceMove<char>)danceMove;
                        programs = Partner(programs, danceMovePartner.ValueA, danceMovePartner.ValueB);
                        break;
                }
            }

            return programs;
        }

        private char[] InitializePrograms()
        {
            char[] programs = new char[totalPrograms];

            int a = (int)'a';
            int j = 0;
            for (int i = a; i < a + totalPrograms; i++)
            {
                programs[j] = (char)i;
                j++;
            }

            return programs;
        }

        private char[] Spin(char[] programs, int moveProgramsFromEnd)
        {
            int n = programs.Length;
            char[] programsAfterSpin = new char[n];
            int position = 0;

            for (int i = n - moveProgramsFromEnd; i < n; i++)
            {
                programsAfterSpin[position] = programs[i];
                position++;
            }

            for (int i = 0; i < n - moveProgramsFromEnd; i++)
            {
                programsAfterSpin[position] = programs[i];
                position++;
            }

            return programsAfterSpin;
        }

        private char[] Exchange(char[] programs, int positionA, int positionB)
        {
            char programA = programs[positionA];
            char programB = programs[positionB];

            programs[positionA] = programB;
            programs[positionB] = programA;

            return programs;
        }

        private char[] Partner(char[] programs, char programA, char programB)
        {
            int? positionA = null;
            int? positionB = null;

            for (int i = 0; i < programs.Length; i++)
            {
                if (programs[i] == programA)
                {
                    positionA = i;
                }

                if (programs[i] == programB)
                {
                    positionB = i;
                }

                if (positionA.HasValue && positionB.HasValue)
                {
                    programs = Exchange(programs, positionA.Value, positionB.Value);
                    break;
                }
            }

            return programs;
        }
    }
}
