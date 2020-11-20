/*
--- Part Two ---

Now that you've helpfully marked up their design documents, it occurs to you
that triangles are specified in groups of three vertically. Each set of three
numbers in a column specifies a triangle. Rows are unrelated.

For example, given the following specification, numbers with the same hundreds
digit would be part of the same triangle:

101 301 501
102 302 502
103 303 503
201 401 601
202 402 602
203 403 603

In your puzzle input, and instead reading by columns, how many of the listed
triangles are possible?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2016.Day3
{
    public class Part2 : ITask<int>
    {
        private readonly DesignDocumentRepository designDocumentRepository;

        private readonly Triangle triangle;

        public Part2()
        {
            designDocumentRepository = new DesignDocumentRepository();
            triangle = new Triangle();
        }

        public int Solution(string input)
        {
            List<Sides> designDocument = designDocumentRepository.ParseInputVertically(input);
            int triangles = triangle.CountTriangles(designDocument);

            return triangles;
        }
    }
}
