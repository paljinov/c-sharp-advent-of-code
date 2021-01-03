/*
--- Day 6: Signals and Noise ---

Something is jamming your communications with Santa. Fortunately, your signal is
only partially jammed, and protocol in situations like this is to switch to a
simple repetition code to get the message through.

In this model, the same message is sent repeatedly. You've recorded the
repeating message signal (your puzzle input), but the data seems quite corrupted
- almost too badly to recover. Almost.

All you need to do is figure out which character is most frequent for each
position. For example, suppose you had recorded the following messages:

eedadn
drvtee
eandsr
raavrd
atevrs
tsrnev
sdttsa
rasrtv
nssdts
ntnada
svetve
tesnvt
vntsnd
vrdear
dvrsen
enarar

The most common character in the first column is e; in the second, a; in the
third, s, and so on. Combining these characters returns the error-corrected
message, easter.

Given the recording in your puzzle input, what is the error-corrected version of
the message being sent?
*/

namespace App.Tasks.Year2016.Day6
{
    public class Part1 : ITask<string>
    {
        private readonly MessagesRepository messagesRepository;

        private readonly Message message;

        public Part1()
        {
            messagesRepository = new MessagesRepository();
            message = new Message();
        }

        public string Solution(string input)
        {
            string[] messages = messagesRepository.GetMessages(input);
            string errorCorrectedMessage = message.GetErrorCorrectedMessageForMostCommonLetter(messages);

            return errorCorrectedMessage;
        }
    }
}
