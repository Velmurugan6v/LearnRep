using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace level_23_puzzle_2
{
    public class SudokuManager : MonoBehaviour
    {

        [SerializeField] private Text[] cellTexts; // Array of UI Text elements representing each Sudoku cell
        private int[,] board;

        private void Start()
        {
            board = new int[9, 9];
            InitializeBoard();
            UpdateUI();
        }

        private void InitializeBoard()
        {
            // Clear the board
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    board[row, col] = 0;
                }
            }

            // Add some initial values to the board (for testing purposes)
            board[0, 1] = 6;
            board[0, 2] = 1;
            board[0, 4] = 4;
            board[0, 5] = 5;
            board[0, 8] = 7;

            // ... Add more initial values if desired ...
        }

        private void UpdateUI()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    int value = board[row, col];
                    int index = row * 9 + col;

                    if (value == 0)
                    {
                        cellTexts[index].text = "";
                    }
                    else
                    {
                        cellTexts[index].text = value.ToString();
                    }
                }
            }
        }

        private bool IsSafe(int row, int col, int num)
        {
            // Check if the number already exists in the row
            for (int i = 0; i < 9; i++)
            {
                if (board[row, i] == num)
                {
                    return false;
                }
            }

            // Check if the number already exists in the column
            for (int i = 0; i < 9; i++)
            {
                if (board[i, col] == num)
                {
                    return false;
                }
            }

            // Check if the number already exists in the 3x3 box
            int boxRow = row - row % 3;
            int boxCol = col - col % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[boxRow + i, boxCol + j] == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool Solve()
        {
            int row = 0;
            int col = 0;
            bool isEmpty = true;

            // Find the first empty cell in the board
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty)
                {
                    break;
                }
            }

            // If all cells are filled, the board is solved
            if (isEmpty)
            {
                return true;
            }

            // Try numbers from 1 to 9
            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(row, col, num))
                {
                    board[row, col] = num;
                    UpdateUI();

                    if (Solve())
                    {
                        return true;
                    }

                    // If the number doesn't lead to a solution, reset it
                    board[row, col] = 0;
                    UpdateUI();
                }
            }

            return false;
        }

        public void SolveSudoku()
        {
            if (Solve())
            {
                Debug.Log("Solved the Sudoku puzzle!");
            }
            else
            {
                Debug.Log("No solution exists for the Sudoku puzzle.");
            }
        }
    }
}
