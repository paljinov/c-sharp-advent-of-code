using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2016.Day21
{
    public class Scrambler
    {
        public string ScramblePassword(List<Operation> operations, string password)
        {
            StringBuilder scrambledPassword = new StringBuilder(password);

            foreach (Operation operation in operations)
            {
                switch (operation.OperationType)
                {
                    case OperationType.SwapPosition:
                        SwapPosition(scrambledPassword, operation);
                        break;
                    case OperationType.SwapLetter:
                        SwapLetter(scrambledPassword, operation);
                        break;
                    case OperationType.ReversePositions:
                        ReversePositions(scrambledPassword, operation);
                        break;
                    case OperationType.Rotate:
                        bool rotateLeft = false;
                        if (operation.FirstArgument == "left")
                        {
                            rotateLeft = true;
                        }

                        int rotateSteps = int.Parse(operation.SecondArgument);

                        Rotate(scrambledPassword, rotateLeft, rotateSteps);
                        break;
                    case OperationType.MovePosition:
                        int moveFromPosition = int.Parse(operation.FirstArgument);
                        int moveToPosition = int.Parse(operation.SecondArgument);

                        MovePosition(scrambledPassword, moveFromPosition, moveToPosition);
                        break;
                    case OperationType.RotateBasedOnPosition:
                        int index = scrambledPassword.ToString().IndexOf(operation.FirstArgument);
                        int rotateBasedOnPositionSteps = 1 + index;
                        if (index >= 4)
                        {
                            rotateBasedOnPositionSteps += 1;
                        }

                        Rotate(scrambledPassword, false, rotateBasedOnPositionSteps);
                        break;
                }
            }

            return scrambledPassword.ToString();
        }

        public string UnscramblePassword(List<Operation> operations, string scrambledPassword)
        {
            operations.Reverse();
            StringBuilder unscrambledPassword = new StringBuilder(scrambledPassword);

            foreach (Operation operation in operations)
            {
                switch (operation.OperationType)
                {
                    case OperationType.SwapPosition:
                        SwapPosition(unscrambledPassword, operation);
                        break;
                    case OperationType.SwapLetter:
                        SwapLetter(unscrambledPassword, operation);
                        break;
                    case OperationType.ReversePositions:
                        ReversePositions(unscrambledPassword, operation);
                        break;
                    case OperationType.Rotate:
                        bool rotateLeft = true;
                        if (operation.FirstArgument == "left")
                        {
                            rotateLeft = false;
                        }

                        int rotateSteps = int.Parse(operation.SecondArgument);

                        Rotate(unscrambledPassword, rotateLeft, rotateSteps);
                        break;
                    case OperationType.MovePosition:
                        int moveFromPosition = int.Parse(operation.SecondArgument);
                        int moveToPosition = int.Parse(operation.FirstArgument);

                        MovePosition(unscrambledPassword, moveFromPosition, moveToPosition);
                        break;
                    case OperationType.RotateBasedOnPosition:
                        int index = unscrambledPassword.ToString().IndexOf(operation.FirstArgument);
                        int rotateBasedOnPositionSteps = (index / 2) + 1;
                        if (index != 0 && index % 2 == 0)
                        {
                            rotateBasedOnPositionSteps += 4;
                        }

                        Rotate(unscrambledPassword, true, rotateBasedOnPositionSteps);
                        break;
                }
            }

            return unscrambledPassword.ToString();
        }

        private void SwapPosition(StringBuilder password, Operation operation)
        {
            int firstPosition = int.Parse(operation.FirstArgument);
            int secondPosition = int.Parse(operation.SecondArgument);
            char firstLetter = password[firstPosition];
            char secondLetter = password[secondPosition];

            password[firstPosition] = secondLetter;
            password[secondPosition] = firstLetter;
        }

        private void SwapLetter(StringBuilder password, Operation operation)
        {
            for (int i = 0; i < password.Length; i++)
            {
                char swapLetter = password[i];

                if (swapLetter == operation.FirstArgument.First())
                {
                    password[i] = operation.SecondArgument.First();
                }
                else if (swapLetter == operation.SecondArgument.First())
                {
                    password[i] = operation.FirstArgument.First();
                }
            }
        }

        private void ReversePositions(StringBuilder password, Operation operation)
        {
            int reverseFrom = int.Parse(operation.FirstArgument);
            int reverseTo = int.Parse(operation.SecondArgument);

            string beforeReversing = password.ToString();
            int j = 0;

            for (int i = reverseFrom; i <= reverseTo; i++)
            {
                password[i] = beforeReversing[reverseTo - j];
                j++;
            }
        }

        private void Rotate(StringBuilder password, bool rotateLeft, int rotateSteps)
        {
            string rotated = password.ToString();

            if (rotateLeft)
            {
                rotateSteps %= rotated.Length;
                rotated = rotated[rotateSteps..] + rotated.Substring(0, rotateSteps);
            }
            else
            {
                rotateSteps %= rotated.Length;
                rotated = rotated[^rotateSteps..] + rotated.Substring(0, rotated.Length - rotateSteps);
            }

            password.Clear();
            password.Append(rotated);
        }

        private void MovePosition(StringBuilder password, int moveFromPosition, int moveToPosition)
        {
            char moveLetter = password[moveFromPosition];
            password.Remove(moveFromPosition, 1);
            password.Insert(moveToPosition, moveLetter);
        }
    }
}
