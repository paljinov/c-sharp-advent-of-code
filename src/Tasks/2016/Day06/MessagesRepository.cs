using System;

namespace App.Tasks.Year2016.Day6
{
    public class MessagesRepository
    {
        public string[] GetMessages(string input)
        {
            string[] messages = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return messages;
        }
    }
}
