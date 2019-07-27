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

namespace Sudoku.Converters
{
    public class CellDraftConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int draftMask = (int)value;
            String txt = "";

            // On a forcément au moins 2 chiffres cochés
            for (int i = 1; i <= 9; i++)
            {
                if ((draftMask & (1 << i)) > 0)
                {
                    if (!String.IsNullOrEmpty(txt))
                        txt += ",";
                    txt += i;
                }
            }


            return txt;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
