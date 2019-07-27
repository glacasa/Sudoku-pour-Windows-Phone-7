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
using System.Windows.Data;
using Sudoku.Model;

namespace Sudoku.Converters
{

    public class CellVisibilityConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Cell cell = value as Cell;

            if (cell != null)
            {
                if (parameter.ToString() == "StartCell" && cell.IsStartCell())
                    return Visibility.Visible;

                if (parameter.ToString() == "Number" && cell.Number > 0 && !cell.IsStartCell())
                    return Visibility.Visible;

                if (parameter.ToString() == "Draft" && cell.Number == 0 && cell.DraftMask > 0)
                    return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
