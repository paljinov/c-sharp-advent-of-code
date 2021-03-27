using System.Text.RegularExpressions;

namespace App.Tasks.Year2017.Day16
{
    public class Dance
    {
        private readonly int totalPrograms = 16;
        private readonly Regex spinRegex = new Regex(@"^s(\d+)$");
        private readonly Regex exchangeRegex = new Regex(@"^x(\d+)\/(\d+)$");
        private readonly Regex partnerRegex = new Regex(@"^p(\w)\/(\w)$");

        public string GetProgramsOrderAfterDance(string[] danceMoves)
        {
            char[] programs = InitializePrograms();

            foreach (string danceMove in danceMoves)
            {
                Match spinMatch = spinRegex.Match(danceMove);
                Match exchangeMatch = exchangeRegex.Match(danceMove);
                Match partnerMatch = partnerRegex.Match(danceMove);

                if (spinMatch.Success)
                {
                    int moveProgramsFromEnd = int.Parse(spinMatch.Groups[1].Value);
                    programs = Spin(programs, moveProgramsFromEnd);
                }
                else if (exchangeMatch.Success)
                {
                    int positionA = int.Parse(exchangeMatch.Groups[1].Value);
                    int positionB = int.Parse(exchangeMatch.Groups[2].Value);
                    programs = Exchange(programs, positionA, positionB);
                }
                else if (partnerMatch.Success)
                {
                    char programA = partnerMatch.Groups[1].Value[0];
                    char programB = partnerMatch.Groups[2].Value[0];
                    programs = Partner(programs, programA, programB);
                }
            }

            return new string(programs);
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
            char[] startPrograms = programs[^moveProgramsFromEnd..];
            char[] endPrograms = programs[0..^moveProgramsFromEnd];

            int i = 0;
            foreach (char program in startPrograms)
            {
                programs[i] = program;
                i++;
            }
            foreach (char program in endPrograms)
            {
                programs[i] = program;
                i++;
            }

            return programs;
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
