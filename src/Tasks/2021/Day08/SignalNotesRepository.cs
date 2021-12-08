using System;

namespace App.Tasks.Year2021.Day8
{
    public class SignalNotesRepository
    {
        public SignalNote[] GetSignalNotes(string input)
        {
            string[] signalNotesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            SignalNote[] signalNotes = new SignalNote[signalNotesString.Length];

            for (int i = 0; i < signalNotesString.Length; i++)
            {
                string[] signalNotesParts = signalNotesString[i].Split('|', StringSplitOptions.RemoveEmptyEntries);
                SignalNote signalNote = new SignalNote
                {
                    SignalPatterns = signalNotesParts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries),
                    Output = signalNotesParts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                };

                signalNotes[i] = signalNote;
            }

            return signalNotes;
        }
    }
}
