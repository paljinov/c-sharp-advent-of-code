using System;
using System.Collections.Generic;

namespace App.Tasks.Year2016.Day3
{
    public class DesignDocumentRepository
    {
        public List<Sides> ParseInput(string input)
        {
            List<Sides> designDocument = new List<Sides>();

            string[] designDocumentString = input.Split(Environment.NewLine);
            foreach (string sidesString in designDocumentString)
            {
                string[] sidesStringSplitted = sidesString.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Sides sides = new Sides
                {
                    A = int.Parse(sidesStringSplitted[0]),
                    B = int.Parse(sidesStringSplitted[1]),
                    C = int.Parse(sidesStringSplitted[2])
                };

                designDocument.Add(sides);
            }

            return designDocument;
        }

        public List<Sides> ParseInputVertically(string input)
        {
            List<Sides> designDocument = new List<Sides>();

            string[] designDocumentString = input.Split(Environment.NewLine);
            for (int i = 0; i < designDocumentString.Length; i += 3)
            {
                string[] sidesStrings = new string[] {
                    designDocumentString[i],
                    designDocumentString[i+1],
                    designDocumentString[i+2]
                };

                List<int[]> triplets = new List<int[]>();
                foreach (string sidesString in sidesStrings)
                {
                    string[] sidesStringSplitted = sidesString.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    triplets.Add(new int[]
                    {
                        int.Parse(sidesStringSplitted[0]),
                        int.Parse(sidesStringSplitted[1]),
                        int.Parse(sidesStringSplitted[2])
                    });
                }

                designDocument.Add(new Sides { A = triplets[0][0], B = triplets[1][0], C = triplets[2][0] });
                designDocument.Add(new Sides { A = triplets[0][1], B = triplets[1][1], C = triplets[2][1] });
                designDocument.Add(new Sides { A = triplets[0][2], B = triplets[1][2], C = triplets[2][2] });
            }

            return designDocument;
        }
    }
}
