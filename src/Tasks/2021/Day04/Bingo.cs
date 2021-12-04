namespace App.Tasks.Year2021.Day4
{
    public class Bingo
    {
        public int CalculateWinningBoardFinalScore(int[] drawnNumbers, int[][,] boards)
        {
            int winningBoardFinalScore = 0;

            bool[][,] markedBoardsNumbers = InitializeMarkedBoardsNumbers(boards);

            foreach (int drawnNumber in drawnNumbers)
            {
                MarkDrawnBoardsNumbers(drawnNumber, boards, markedBoardsNumbers);
                int? winnerBoardIndex = FindWinnerBoardIndex(markedBoardsNumbers);

                if (winnerBoardIndex.HasValue)
                {
                    int sumOfAllUnmarkedNumbers = CalculateSumOfAllUnmarkedNumbers(
                        boards[winnerBoardIndex.Value], markedBoardsNumbers[winnerBoardIndex.Value]);
                    winningBoardFinalScore = drawnNumber * sumOfAllUnmarkedNumbers;
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

        private void MarkDrawnBoardsNumbers(int drawnNumber, int[][,] boards, bool[][,] markedBoardsNumbers)
        {
            for (int i = 0; i < boards.Length; i++)
            {
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

        private int? FindWinnerBoardIndex(bool[][,] markedBoardsNumbers)
        {
            for (int i = 0; i < markedBoardsNumbers.Length; i++)
            {
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
                        return i;
                    }
                }

                for (int k = 0; k < markedBoardNumbers.GetLength(1); k++)
                {
                    // If all numbers in a column are marked
                    if (allNumbersInColumnsMarked[k])
                    {
                        return i;
                    }
                }
            }

            return null;
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
