using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sudoku.Resources;

namespace Sudoku
{
    public partial class MainPage : PhoneApplicationPage
    {
        private static TheGrid theSudokuGrid;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }


  


        //Navigation

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (theSudokuGrid == null)
            {
                theSudokuGrid = new TheGrid();
            }
            ContentPanel.Children.Add(theSudokuGrid);
            

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            ContentPanel.Children.Clear();
        }

        
 


    }
}