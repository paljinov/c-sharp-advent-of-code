using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2021.Day16;
using Xunit;

namespace Tests.Tasks.Year2021.Day16
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(HexadecimalTransmission_SumOfAllPacketsVersionNumbers_TestData))]
        public void Solution_HexadecimalTransmissionExample_SumOfAllPacketsVersionNumbersEquals(
            string hexadecimalTransmission,
            int sumOfAllPacketsVersionNumbers
        )
        {
            Assert.Equal(sumOfAllPacketsVersionNumbers, task.Solution(hexadecimalTransmission));
        }

        public class HexadecimalTransmission_SumOfAllPacketsVersionNumbers_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "D2FE28",
                    6
                };

                yield return new object[] {
                    "38006F45291200",
                    9
                };

                yield return new object[] {
                    "EE00D40C823060",
                    14
                };

                yield return new object[] {
                    "8A004A801A8002F478",
                    16
                };

                yield return new object[] {
                    "620080001611562C8802118E34",
                    12
                };

                yield return new object[] {
                    "C0015000016115A2E0802F182340",
                    23
                };

                yield return new object[] {
                    "A0016C880162017C3686B18A3D4780",
                    31
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
