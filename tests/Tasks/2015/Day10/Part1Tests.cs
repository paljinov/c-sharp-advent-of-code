using System.Reflection;
using App.Tasks.Year2015.Day10;
using Xunit;

namespace Tests.Tasks.Year2015.Day10
{
    public class Part1Tests
    {
        private readonly LookAndSay lookAndSay;

        private readonly MethodInfo methodInfo;

        public Part1Tests()
        {
            lookAndSay = new LookAndSay();

            methodInfo = lookAndSay.GetType()
                .GetMethod("GenerateSequence", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [Theory]
        [InlineData("1", "11")]
        [InlineData("11", "21")]
        [InlineData("21", "1211")]
        [InlineData("1211", "111221")]
        [InlineData("111221", "312211")]
        public void Solution_InputSequenceExample_ResultSequenceEquals(string inputSequence, string resultSequence)
        {
            Assert.Equal(resultSequence, methodInfo.Invoke(lookAndSay, new object[] { inputSequence }));
        }
    }
}
