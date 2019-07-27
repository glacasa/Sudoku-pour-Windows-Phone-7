using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Sudoku.Model;

namespace Sudoku.Controllers
{
    public class GameController
    {
        private static SudokuGrid _currentGame;

        public static SudokuGrid CurrentGame
        {
            get
            {
                if (_currentGame ==null)
                {
                    _currentGame = NewGame();
                }
                return _currentGame;
            }
            set { _currentGame = value; }
        }


        public static SudokuGrid NewGame()
        {
            Random r = new Random();
            var grid = new SudokuGrid();

            List<int> list = new List<int>(9);
            for (int i = 0; i < 9; i++)
            {
                list.Insert(r.Next(list.Count + 1), i + 1);
            }

            //Grille de base)
            for (int i = 0; i < 9; i++)
            {
                grid[i, 0].Cell.StartCell = list[i];
                grid[i, 1].Cell.StartCell = list[(i + 3) % 9];
                grid[i, 2].Cell.StartCell = list[(i + 6) % 9];

                grid[i, 3].Cell.StartCell = list[(i + 1) % 9];
                grid[i, 4].Cell.StartCell = list[(i + 4) % 9];
                grid[i, 5].Cell.StartCell = list[(i + 7) % 9];

                grid[i, 6].Cell.StartCell = list[(i + 2) % 9];
                grid[i, 7].Cell.StartCell = list[(i + 5) % 9];
                grid[i, 8].Cell.StartCell = list[(i + 8) % 9];
            }


            //On mélange les lignes/colonnes
            int n;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    n = r.Next(3);
                    grid.InvertLines(i + (3 * j), n + (3 * j));
                    n = r.Next(3);
                    grid.InvertCols(i + (3 * j), n + (3 * j));
                }
            }

            //On efface les cases
            int nbCases = 9 * 9;
            int[] byNumber = { 9, 9, 9, 9, 9, 9, 9, 9, 9 };
            int[] bySquare = { 9, 9, 9, 9, 9, 9, 9, 9, 9 };

            Cell c;
            while (nbCases > 35)
            {
                int x = r.Next(9);
                int y = r.Next(9);
                int sqNumber = (x / 3) * 3 + y / 3;
                c = grid[x, y].Cell;
                if (c.IsStartCell() && byNumber[c.Number - 1] > 2 && bySquare[sqNumber] > 2 && (sqNumber != 4 || bySquare[4]>3))
                {
                    nbCases--;
                    byNumber[c.Number - 1]--;
                    bySquare[sqNumber]--;
                    c.StartCell = 0;

                    if (x != 4 || y != 4)
                    {
                        x = 8 - x;
                        y = 8 - y;
                        sqNumber = (x / 3) * 3 + y / 3;
                        c = grid[x, y].Cell;

                        nbCases--;
                        byNumber[c.Number - 1]--;
                        bySquare[sqNumber]--;
                        c.StartCell = 0;
                    }
                }


            }
            
            IsolatedStorageHelper.SaveObject("sudoku", grid);

            return grid;
        }



    }
}
