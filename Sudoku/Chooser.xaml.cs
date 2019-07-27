using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Sudoku.Model;

namespace Sudoku
{
    public partial class Chooser : UserControl
    {
        public event EventHandler Chosen;

        public CellVm CellVm
        {
            get
            {
                return DataContext as CellVm;
            }
            set
            {
                DataContext = value;
            }
        }

        private Cell Cell
        {
            get
            {
                return CellVm.Cell;
            }
        }

        private bool _draftMode;

        public Chooser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_draftMode)
            {
                ToggleButton b = sender as ToggleButton;
                int n = int.Parse(b.Content.ToString());

                Cell.Number = n;
                Cell.DraftMask = 0;

                if (Chosen != null)
                    Chosen(this, new EventArgs());
            }
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int nbItemsChecked = 0;
            int itemChecked = 0;
            int mask = 0;
            foreach (ToggleButton button in buttonGrid.Children)
            {
                if (button.IsChecked.HasValue && button.IsChecked.Value)
                {
                    int n = int.Parse(button.Content.ToString());
                    nbItemsChecked++;
                    itemChecked = n;

                    mask = mask | (1 << n);
                }
            }

            Cell.Number = 0;
            Cell.DraftMask = mask;

            if (Chosen != null)
                Chosen(this, new EventArgs());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Cell.Number = 0;
            Cell.DraftMask = 0;

            if (Chosen != null)
                Chosen(this, new EventArgs());
        }

        internal void SetDataContext(CellVm cell, bool draftMode)
        {
            CellVm = cell;
            _draftMode = draftMode;

            if (_draftMode)
            {
                btSave.Visibility = Visibility.Visible;
                btCancel.Visibility = Visibility.Collapsed;
            }
            else
            {
                btSave.Visibility = Visibility.Collapsed;
                btCancel.Visibility = Visibility.Visible;
            }

            foreach (ToggleButton button in buttonGrid.Children)
            {
                int n = int.Parse(button.Content.ToString());
                if (cell.Cell.Number == n || (cell.Cell.DraftMask & (1 << n)) > 0)
                {
                    button.IsChecked = true;
                }
                else
                {
                    button.IsChecked = false;
                }
            }
        }
    }
}
