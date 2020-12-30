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

        [Fact]
        public void Solution_FirstSequenceExample_ResultSequenceEquals()
        {
            Assert.Equal("11", methodInfo.Invoke(lookAndSay, new object[] { "1" }));
        }

        [Fact]
        public void Solution_SecondSequenceExample_ResultSequenceEquals()
        {
            Assert.Equal("21", methodInfo.Invoke(lookAndSay, new object[] { "11" }));
        }

        [Fact]
        public void Solution_ThirdSequenceExample_ResultSequenceEquals()
        {
            Assert.Equal("1211", methodInfo.Invoke(lookAndSay, new object[] { "21" }));
        }

        [Fact]
        public void Solution_FourthSequenceExample_ResultSequenceEquals()
        {
            Assert.Equal("111221", methodInfo.Invoke(lookAndSay, new object[] { "1211" }));
        }

        [Fact]
        public void Solution_FifthSequenceExample_ResultSequenceEquals()
        {
            Assert.Equal("312211", methodInfo.Invoke(lookAndSay, new object[] { "111221" }));
        }
    }
}
