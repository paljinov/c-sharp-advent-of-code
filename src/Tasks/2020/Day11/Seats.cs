using System;

namespace App.Tasks.Year2020.Day11
{
    public class OccupiedSeatsCounter
    {
        private const char EMPTY_SEAT = 'L';

        private const char OCCUPIED_SEAT = '#';

        private const int OCCUPIED_FOR_ADJACENT_EMPTY_TO_OCCUPIED_SEAT_CHANGE = 0;

        private const int OCCUPIED_FOR_ADJACENT_OCCUPIED_TO_EMPTY_SEAT_CHANGE = 4;

        private const int OCCUPIED_FOR_FIRST_SEEN_EMPTY_TO_OCCUPIED_SEAT_CHANGE = 0;

        private const int OCCUPIED_FOR_FIRST_SEEN_OCCUPIED_TO_EMPTY_SEAT_CHANGE = 5;

        public int CountFinallyOccupiedSeatsForAdjacentSeatsDecision(char[,] seats)
        {
            bool changed = true;
            while (changed)
            {
                changed = false;

                char[,] seatsIteration = seats.Clone() as char[,];
                for (int i = 0; i < seats.GetLength(0); i++)
                {
                    for (int j = 0; j < seats.GetLength(1); j++)
                    {
                        int occupiedNeighbours = CountOccupiedAdjacentSeatsForPosition(seats, i, j);

                        if (seats[i, j] == EMPTY_SEAT
                            && occupiedNeighbours == OCCUPIED_FOR_ADJACENT_EMPTY_TO_OCCUPIED_SEAT_CHANGE)
                        {
                            seatsIteration[i, j] = OCCUPIED_SEAT;
                            changed = true;
                        }
                        else if (seats[i, j] == OCCUPIED_SEAT
                            && occupiedNeighbours >= OCCUPIED_FOR_ADJACENT_OCCUPIED_TO_EMPTY_SEAT_CHANGE)
                        {
                            seatsIteration[i, j] = EMPTY_SEAT;
                            changed = true;
                        }
                    }
                }

                seats = seatsIteration;
            }

            int occupiedSeats = CountOccupiedSeats(seats);

            return occupiedSeats;
        }

        public int CountFinallyOccupiedSeatsForFirstSeenSeatsDecision(char[,] seats)
        {
            bool changed = true;
            while (changed)
            {
                changed = false;

                char[,] seatsIteration = seats.Clone() as char[,];
                for (int i = 0; i < seats.GetLength(0); i++)
                {
                    for (int j = 0; j < seats.GetLength(1); j++)
                    {
                        int occupiedFirstSeen = CountFirstSeenSeatsFromPosition(seats, i, j);

                        if (seats[i, j] == EMPTY_SEAT
                            && occupiedFirstSeen == OCCUPIED_FOR_FIRST_SEEN_EMPTY_TO_OCCUPIED_SEAT_CHANGE)
                        {
                            seatsIteration[i, j] = OCCUPIED_SEAT;
                            changed = true;
                        }
                        else if (seats[i, j] == OCCUPIED_SEAT
                            && occupiedFirstSeen >= OCCUPIED_FOR_FIRST_SEEN_OCCUPIED_TO_EMPTY_SEAT_CHANGE)
                        {
                            seatsIteration[i, j] = EMPTY_SEAT;
                            changed = true;
                        }
                    }
                }

                seats = seatsIteration;
            }

            int occupiedSeats = CountOccupiedSeats(seats);

            return occupiedSeats;
        }

        private int CountOccupiedAdjacentSeatsForPosition(char[,] seats, int i, int j)
        {
            int occupiedNeighbours = 0;

            int rows = seats.GetLength(0);
            int columns = seats.GetLength(1);

            for (int k = Math.Max(i - 1, 0); k <= Math.Min(i + 1, rows - 1); k++)
            {
                for (int h = Math.Max(j - 1, 0); h <= Math.Min(j + 1, columns - 1); h++)
                {
                    if (i == k && j == h)
                    {
                        continue;
                    }

                    if (seats[k, h] == OCCUPIED_SEAT)
                    {
                        occupiedNeighbours++;
                    }
                }
            }

            return occupiedNeighbours;
        }

        private int CountFirstSeenSeatsFromPosition(char[,] seats, int i, int j)
        {
            int occupiedFirstSeen = 0;

            int rows = seats.GetLength(0);
            int columns = seats.GetLength(1);

            // UP-LEFT
            int k = i - 1;
            int h = j - 1;
            while (k >= 0 && h >= 0)
            {
                if (seats[k, h] == OCCUPIED_SEAT)
                {
                    occupiedFirstSeen++;
                    break;
                }
                else if (seats[k, h] == EMPTY_SEAT)
                {
                    break;
                }

                k--;
                h--;
            }

            // UP
            k = i - 1;
            h = j;
            while (k >= 0)
            {
                if (seats[k, h] == OCCUPIED_SEAT)
                {
                    occupiedFirstSeen++;
                    break;
                }
                else if (seats[k, h] == EMPTY_SEAT)
                {
                    break;
                }

                k--;
            }

            // UP-RIGHT
            k = i - 1;
            h = j + 1;
            while (k >= 0 && h < columns)
            {
                if (seats[k, h] == OCCUPIED_SEAT)
                {
                    occupiedFirstSeen++;
                    break;
                }
                else if (seats[k, h] == EMPTY_SEAT)
                {
                    break;
                }

                k--;
                h++;
            }

            // RIGHT
            k = i;
            h = j + 1;
            while (h < columns)
            {
                if (seats[k, h] == OCCUPIED_SEAT)
                {
                    occupiedFirstSeen++;
                    break;
                }
                else if (seats[k, h] == EMPTY_SEAT)
                {
                    break;
                }

                h++;
            }

            // DOWN-RIGHT
            k = i + 1;
            h = j + 1;
            while (k < rows && h < columns)
            {
                if (seats[k, h] == OCCUPIED_SEAT)
                {
                    occupiedFirstSeen++;
                    break;
                }
                else if (seats[k, h] == EMPTY_SEAT)
                {
                    break;
                }

                k++;
                h++;
            }

            // DOWN
            k = i + 1;
            h = j;
            while (k < rows)
            {
                if (seats[k, h] == OCCUPIED_SEAT)
                {
                    occupiedFirstSeen++;
                    break;
                }
                else if (seats[k, h] == EMPTY_SEAT)
                {
                    break;
                }

                k++;
            }

            // DOWN-LEFT
            k = i + 1;
            h = j - 1;
            while (k < rows && h >= 0)
            {
                if (seats[k, h] == OCCUPIED_SEAT)
                {
                    occupiedFirstSeen++;
                    break;
                }
                else if (seats[k, h] == EMPTY_SEAT)
                {
                    break;
                }

                k++;
                h--;
            }

            // LEFT
            k = i;
            h = j - 1;
            while (h >= 0)
            {
                if (seats[k, h] == OCCUPIED_SEAT)
                {
                    occupiedFirstSeen++;
                    break;
                }
                else if (seats[k, h] == EMPTY_SEAT)
                {
                    break;
                }

                h--;
            }

            return occupiedFirstSeen;
        }

        private int CountOccupiedSeats(char[,] seats)
        {
            int occupiedSeats = 0;

            for (int i = 0; i < seats.GetLength(0); i++)
            {
                for (int j = 0; j < seats.GetLength(1); j++)
                {
                    if (seats[i, j] == OCCUPIED_SEAT)
                    {
                        occupiedSeats++;
                    }
                }
            }

            return occupiedSeats;
        }
    }
}
