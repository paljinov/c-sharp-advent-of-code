using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day21
{
    public class OperationsRepository
    {
        public List<Operation> GetOperations(string input)
        {
            List<Operation> operations = new List<Operation>();

            Regex swapPositionRegex = new Regex(@"^swap\sposition\s(\d+)\swith\sposition\s(\d+)$");
            Regex swapLetterRegex = new Regex(@"^swap\sletter\s([a-z])\swith\sletter\s([a-z])$");
            Regex reversePositionsRegex = new Regex(@"^reverse\spositions\s(\d+)\sthrough\s(\d+)$");
            Regex rotateRegex = new Regex(@"^rotate\s(left|right)\s(\d+)\ssteps?$");
            Regex movePositionRegex = new Regex(@"^move\sposition\s(\d+)\sto\sposition\s(\d+)$");
            Regex rotateBasedOnPositionRegex = new Regex(@"^rotate\sbased\son\sposition\sof\sletter\s([a-z])$");

            string[] operationsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string operationString in operationsString)
            {
                OperationType operationType = 0;
                string firstArgument = string.Empty;
                string secondArgument = string.Empty;

                Match swapPositionMatch = swapPositionRegex.Match(operationString);
                if (swapPositionMatch.Success)
                {
                    operationType = OperationType.SwapPosition;
                    firstArgument = swapPositionMatch.Groups[1].Value;
                    secondArgument = swapPositionMatch.Groups[2].Value;
                }

                if (string.IsNullOrEmpty(firstArgument))
                {
                    Match swapLetterMatch = swapLetterRegex.Match(operationString);
                    if (swapLetterMatch.Success)
                    {
                        operationType = OperationType.SwapLetter;
                        firstArgument = swapLetterMatch.Groups[1].Value;
                        secondArgument = swapLetterMatch.Groups[2].Value;
                    }
                }

                if (string.IsNullOrEmpty(firstArgument))
                {
                    Match reversePositionsMatch = reversePositionsRegex.Match(operationString);
                    if (reversePositionsMatch.Success)
                    {
                        operationType = OperationType.ReversePositions;
                        firstArgument = reversePositionsMatch.Groups[1].Value;
                        secondArgument = reversePositionsMatch.Groups[2].Value;
                    }
                }

                if (string.IsNullOrEmpty(firstArgument))
                {
                    Match rotateMatch = rotateRegex.Match(operationString);
                    if (rotateMatch.Success)
                    {
                        operationType = OperationType.Rotate;
                        firstArgument = rotateMatch.Groups[1].Value;
                        secondArgument = rotateMatch.Groups[2].Value;
                    }
                }

                if (string.IsNullOrEmpty(firstArgument))
                {
                    Match movePositionMatch = movePositionRegex.Match(operationString);
                    if (movePositionMatch.Success)
                    {
                        operationType = OperationType.MovePosition;
                        firstArgument = movePositionMatch.Groups[1].Value;
                        secondArgument = movePositionMatch.Groups[2].Value;
                    }
                }

                if (string.IsNullOrEmpty(firstArgument))
                {
                    Match rotateBasedOnPositionMatch = rotateBasedOnPositionRegex.Match(operationString);
                    if (rotateBasedOnPositionMatch.Success)
                    {
                        operationType = OperationType.RotateBasedOnPosition;
                        firstArgument = rotateBasedOnPositionMatch.Groups[1].Value;
                    }
                }

                Operation operation = new Operation
                {
                    OperationType = operationType,
                    FirstArgument = firstArgument,
                    SecondArgument = secondArgument
                };

                operations.Add(operation);
            }

            return operations;
        }
    }
}
