using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Sudoku.Controllers;
using Sudoku.Model;
using Sudoku.Resources;

namespace Sudoku
{
    public partial class TheGrid : Grid
    {

        public SudokuGrid SudokuGrid
        {
            get
            {
                return DataContext as SudokuGrid;
            }
            set
            {
                DataContext = value;
                GameController.CurrentGame = value;
            }
        }

        private Square[,] squares;

        public TheGrid()
        {
            InitializeComponent();


            SudokuGrid = GameController.CurrentGame;

            // Cadrillage
            for (int i = 0; i < 19; i += 2)
            {
                Rectangle rectCol = new Rectangle();
                rectCol.SetValue(Grid.ColumnProperty, i);
                rectCol.SetValue(Grid.RowSpanProperty, 19);
                rectCol.Fill = new SolidColorBrush(Colors.Black);

                sudokuGrid.Children.Insert(0, rectCol);

                Rectangle rectRow = new Rectangle();
                rectRow.SetValue(Grid.RowProperty, i);
                rectRow.SetValue(Grid.ColumnSpanProperty, 19);
                rectRow.Fill = new SolidColorBrush(Colors.Black);
                sudokuGrid.Children.Insert(0, rectRow);
            }

            // Cases
            squares = new Square[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    int gridCol = i * 2 + 1;
                    int gridRow = j * 2 + 1;

                    Square square = new Square();
                    square.SetValue(Grid.ColumnProperty, gridCol);
                    square.SetValue(Grid.RowProperty, gridRow);
                    sudokuGrid.Children.Add(square);
                    square.DataContext = SudokuGrid[i, j];
                    squares[i, j] = square;
                    square.Click += new RoutedEventHandler(square_Click);
                }

        }

        void square_Click(object sender, RoutedEventArgs e)
        {
            Square sq = sender as Square;
            Cell c = sq.DataContext as Cell;
            SudokuGrid.SelectedCell = c.ViewModel;

        }

 



        public void NewGame()
        {
            SudokuGrid = GameController.NewGame();

            //Bind the cells
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    squares[i, j].DataContext = SudokuGrid[i, j];
                }

        }


        //TODO : vérifier le sudoku à chaque modification d'une case
        //void CloseSubgrid_Completed(object sender, EventArgs e)
        //{
        //    if (SudokuGrid.CheckSudoku())
        //    {
        //        //Sudoku réussi
        //        MessageBox.Show(TextResource.Congratulations);
        //        NewGame();
        //    }
        //}



 



        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(TextResource.NewGameConfirm, TextResource.NewGame, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                NewGame();
        }

        private void DraftButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                String content = b.Content.ToString();
                if (content == "X")
                {
                    SudokuGrid.SelectedCell.Cell.DraftMask = 0;
                }
                else
                {
                    int n = 1 << int.Parse(content);
                    SudokuGrid.SelectedCell.Cell.DraftMask = SudokuGrid.SelectedCell.Cell.DraftMask ^ n;
                }

            }
        }

        private void SelectNumber_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                String content = b.Content.ToString();
                if (content == "X")
                {
                    SudokuGrid.SelectedCell.Cell.Number = 0;
                }
                else
                {
                    SudokuGrid.SelectedCell.Cell.Number = int.Parse(content);
                }

            }
        }


    }
}
