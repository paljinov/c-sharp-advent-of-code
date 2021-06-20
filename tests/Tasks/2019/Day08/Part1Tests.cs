using System.Reflection;
using App.Tasks.Year2019.Day8;
using Xunit;

namespace Tests.Tasks.Year2019.Day8
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            PasswordImage passwordImage = new PasswordImage();
            typeof(PasswordImage)
                .GetField("imageWidth", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(passwordImage, 3);
            typeof(PasswordImage)
                .GetField("imageHeight", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(passwordImage, 2);
            typeof(Part1)
                .GetField("passwordImage", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, passwordImage);
        }

        [Fact]
        public void Solution_PasswordExample_NumberOneAndNumberTwoDigitsProductOfLayerThatContainsFewestZeroDigitsEquals()
        {
            string password = "123456789012";
            Assert.Equal(1, task.Solution(password));
        }
    }
}
