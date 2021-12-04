using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day4
{
    public class Bingo
    {
        public int CalculateBoardFinalScore(int[] drawnNumbers, int[][,] boards, bool lastBoard = false)
        {
            int winningBoardFinalScore = 0;

            bool[][,] markedBoardsNumbers = InitializeMarkedBoardsNumbers(boards);
            List<int> winnerBoardsIndexes = new List<int>();
            int previousWinnersCount = 0;

            foreach (int drawnNumber in drawnNumbers)
            {
                MarkDrawnBoardsNumbers(drawnNumber, boards, markedBoardsNumbers, winnerBoardsIndexes);
                FindWinnerBoardsIndexes(markedBoardsNumbers, winnerBoardsIndexes);

                // If new winner boards are found
                if (winnerBoardsIndexes.Count > previousWinnersCount)
                {
                    int winnerBoardIndex = winnerBoardsIndexes.First();
                    if (lastBoard)
                    {
                        winnerBoardIndex = winnerBoardsIndexes.Last();
                    }

                    int sumOfAllUnmarkedNumbers = CalculateSumOfAllUnmarkedNumbers(
                        boards[winnerBoardIndex], markedBoardsNumbers[winnerBoardIndex]);
                    winningBoardFinalScore = drawnNumber * sumOfAllUnmarkedNumbers;

                    previousWinnersCount = winnerBoardsIndexes.Count;

                    // If interested in winning board
                    if (lastBoard == false)
                    {
                        break;
                    }
                }

                // If all boards are winners
                if (winnerBoardsIndexes.Count == boards.Length)
                {
                    break;
                }
            }

            return winningBoardFinalScore;
        }

        private bool[][,] InitializeMarkedBoardsNumbers(int[][,] boards)
        {
            bool[][,] markedBoardsNumbers = new bool[boards.Length][,];
            int boardSize = boards[0].GetLength(0);

            for (int i = 0; i < boards.Length; i++)
            {
                markedBoardsNumbers[i] = new bool[boardSize, boardSize];
            }

            return markedBoardsNumbers;
        }

        private void MarkDrawnBoardsNumbers(
            int drawnNumber,
            int[][,] boards,
            bool[][,] markedBoardsNumbers,
            List<int> winnerBoardsIndexes
        )
        {
            for (int i = 0; i < boards.Length; i++)
            {
                // Skip winner boards
                if (winnerBoardsIndexes.Contains(i))
                {
                    continue;
                }

                int[,] board = boards[i];

                for (int j = 0; j < board.GetLength(0); j++)
                {
                    for (int k = 0; k < board.GetLength(1); k++)
                    {
                        if (drawnNumber == board[j, k])
                        {
                            markedBoardsNumbers[i][j, k] = true;
                        }
                    }
                }
            }
        }

        private void FindWinnerBoardsIndexes(bool[][,] markedBoardsNumbers, List<int> winnerBoardsIndexes)
        {
            for (int i = 0; i < markedBoardsNumbers.Length; i++)
            {
                // Skip winner boards
                if (winnerBoardsIndexes.Contains(i))
                {
                    continue;
                }

                bool[,] markedBoardNumbers = markedBoardsNumbers[i];
                bool[] allNumbersInColumnsMarked = new bool[markedBoardNumbers.GetLength(1)];
                for (int k = 0; k < markedBoardNumbers.GetLength(1); k++)
                {
                    allNumbersInColumnsMarked[k] = true;
                }

                for (int j = 0; j < markedBoardNumbers.GetLength(0); j++)
                {
                    bool allNumbersInRowMarked = true;
                    for (int k = 0; k < markedBoardNumbers.GetLength(1); k++)
                    {
                        if (markedBoardNumbers[j, k] == false)
                        {
                            allNumbersInRowMarked = false;
                            allNumbersInColumnsMarked[k] = false;
                        }
                    }

                    // If all numbers in a row are marked
                    if (allNumbersInRowMarked)
                    {
                        if (!winnerBoardsIndexes.Contains(i))
                        {
                            winnerBoardsIndexes.Add(i);
                        }
                        break;
                    }
                }

                for (int k = 0; k < markedBoardNumbers.GetLength(1); k++)
                {
                    // If all numbers in a column are marked
                    if (allNumbersInColumnsMarked[k])
                    {
                        if (!winnerBoardsIndexes.Contains(i))
                        {
                            winnerBoardsIndexes.Add(i);
                        }
                        break;
                    }
                }
            }
        }

        private int CalculateSumOfAllUnmarkedNumbers(int[,] board, bool[,] markedBoardNumbers)
        {
            int sumOfAllUnmarkedNumbers = 0;

            for (int i = 0; i < markedBoardNumbers.GetLength(0); i++)
            {
                for (int j = 0; j < markedBoardNumbers.GetLength(1); j++)
                {
                    if (markedBoardNumbers[i, j] == false)
                    {
                        sumOfAllUnmarkedNumbers += board[i, j];
                    }
                }
            }

            return sumOfAllUnmarkedNumbers;
        }
    }
}
