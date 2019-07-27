using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Sudoku.Model
{
    public class SudokuGrid : INotifyPropertyChanged
    {
        private Cell[,] cells;

        public CellVm this[int x, int y]
        {
            get
            {
                return cells[x, y].ViewModel;
            }
        }

        private CellVm _selectedCell;
        public CellVm SelectedCell
        {
            get { return _selectedCell; }
            set
            {
                if (_selectedCell != null)
                    _selectedCell.IsSelected = false;
                _selectedCell = value;
                _selectedCell.IsSelected = true;
                OnPropertyChanged("SelectedCell");
            }
        }

        public String Cells
        {
            get
            {
                String cellsStr = "";
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Cell c = cells[i, j];
                        cellsStr += c.StartCell + "," + c.Number + "," + c.DraftMask + "|";
                    }
                }
                return cellsStr;
            }
            set
            {
                int i = 0, j = 0;
                String[] allCells = value.Split('|');
                foreach (var cell in allCells)
                {
                    string[] values = cell.Split(',');
                    Cell c = new Cell();
                    c.Number = int.Parse(values[1]);
                    c.DraftMask = int.Parse(values[2]);
                    c.StartCell = int.Parse(values[0]);

                    cells[i, j] = c;
                    j++;
                    if (j == 9)
                    {
                        j = 0;
                        i++;
                        if (i == 9)
                            return;
                    }
                }
            }
        }


        public SudokuGrid()
        {
            cells = new Cell[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = new Cell();
                }
            }
        }

        public void InvertCols(int firstCol, int secondCol)
        {
            Cell tempCell;
            for (int i = 0; i < 9; i++)
            {
                tempCell = cells[firstCol, i];
                cells[firstCol, i] = cells[secondCol, i];
                cells[secondCol, i] = tempCell;
            }
        }

        public void InvertLines(int firstLine, int secondLine)
        {
            Cell tempCell;
            for (int i = 0; i < 9; i++)
            {
                tempCell = cells[i, firstLine];
                cells[i, firstLine] = cells[i, secondLine];
                cells[i, secondLine] = tempCell;
            }
        }


        public bool CheckSudoku()
        {
            // on vérifie chaque ligne
            for (int i = 0; i < 9; i++)
            {
                bool[] check = new bool[9];
                for (int j = 0; j < 9; j++)
                {
                    int val = cells[i, j].Number;
                    if (val == 0)
                        return false;
                    if (check[val - 1])
                        return false;
                    check[val - 1] = true;
                }
            }

            // on vérifie chaque colonne
            for (int i = 0; i < 9; i++)
            {
                bool[] check = new bool[9];
                for (int j = 0; j < 9; j++)
                {
                    int val = cells[j, i].Number;
                    if (check[val - 1])
                        return false;
                    check[val - 1] = true;
                }
            }

            // on vérifie chaque carré de 9 cases
            for (int a = 0; a < 7; a += 3)
            {
                for (int b = 0; b < 7; b += 3)
                {

                    //Pour chaque carré :
                    bool[] check = new bool[9];
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            int val = cells[a + i, b + j].Number;
                            if (check[val - 1])
                                return false;
                            check[val - 1] = true;
                        }
                    }

                }
            }

            return true;
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
