using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2021.Day16;
using Xunit;

namespace Tests.Tasks.Year2021.Day16
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [ClassData(typeof(HexadecimalTransmission_EvaluatedExpressionResult_TestData))]
        public void Solution_HexadecimalTransmissionExample_EvaluatedExpressionResultEquals(
            string hexadecimalTransmission,
            long evaluatedExpressionResult
        )
        {
            Assert.Equal(evaluatedExpressionResult, task.Solution(hexadecimalTransmission));
        }

        public class HexadecimalTransmission_EvaluatedExpressionResult_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "C200B40A82",
                    3
                };

                yield return new object[] {
                    "04005AC33890",
                    54
                };

                yield return new object[] {
                    "880086C3E88112",
                    7
                };

                yield return new object[] {
                    "CE00C43D881120",
                    9
                };

                yield return new object[] {
                    "D8005AC2A8F0",
                    1
                };

                yield return new object[] {
                    "F600BC2D8F",
                    0
                };

                yield return new object[] {
                    "9C005AC2F8F0",
                    0
                };

                yield return new object[] {
                    "9C0141080250320F1802104A08",
                    1
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
