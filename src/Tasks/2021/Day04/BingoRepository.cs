using System;
using System.Linq;

namespace App.Tasks.Year2021.Day4
{
    public class BingoRepository
    {
        private const int BOARD_SIZE = 5;

        public int[] GetDrawnNumbers(string input)
        {
            string[] inputRows = input.Split(Environment.NewLine);
            string[] drawnNumbersString = inputRows[0].Split(',');

            int[] drawnNumbers = new int[drawnNumbersString.Length];
            for (int i = 0; i < drawnNumbersString.Length; i++)
            {
                drawnNumbers[i] = int.Parse(drawnNumbersString[i]);
            }

            return drawnNumbers;
        }

        public int[][,] GetBoards(string input)
        {
            string[] inputRows = input.Split(Environment.NewLine);

            int boardsCount = inputRows.Count(x => x == string.Empty);
            int[][,] boards = new int[boardsCount][,];

            int[,] board = new int[BOARD_SIZE, BOARD_SIZE];
            int boardIndex = 0;
            int boardRow = 0;

            for (int i = 2; i < inputRows.Length; i++)
            {
                // If empty row
                if (inputRows[i] == string.Empty)
                {
                    boards[boardIndex] = board;

                    board = new int[BOARD_SIZE, BOARD_SIZE];
                    boardIndex++;
                    boardRow = 0;
                }
                // If board row
                else
                {
                    string[] rowNumbers = inputRows[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    for (int boardColumn = 0; boardColumn < rowNumbers.Length; boardColumn++)
                    {
                        board[boardRow, boardColumn] = int.Parse(rowNumbers[boardColumn]);
                    }

                    // If board is filled with numbers
                    if (boardRow == BOARD_SIZE - 1)
                    {
                        boards[boardIndex] = board;
                    }

                    boardRow++;
                }
            }

            return boards;
        }
    }
}
