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
    public class CellVm : INotifyPropertyChanged
    {

        private Cell _cell;

        public Cell Cell
        {
            get { return _cell; }
            set
            {
                //ignore if values are equal
                if (value == _cell) return;

                _cell = value;
                OnPropertyChanged("Cell");
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
                OnPropertyChanged("IsSelectedInt");
            }
        }

        public CellVm(Cell cell)
        {
            _cell = cell;
            cell.PropertyChanged += new PropertyChangedEventHandler(cell_PropertyChanged);
        }

        void cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Cell");
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

    public class Cell : INotifyPropertyChanged
    {
        private CellVm _viewModel;
        public CellVm ViewModel
        {
            get
            {
                if (_viewModel == null)
                    _viewModel = new CellVm(this);
                return _viewModel;
            }
        }

        private int _startCell;
        public int StartCell
        {
            get
            {
                return _startCell;
            }
            set
            {
                if (_startCell != value)
                {
                    _startCell = value;
                    OnPropertyChanged("StartCell");
                }
            }
        }

        private int _number;
        public int Number
        {
            get
            {
                if (IsStartCell())
                    return StartCell;
                return _number;
            }
            set
            {
                if (IsStartCell())
                    throw new Exception("Vous ne pouvez pas changer le contenu d'une cellule de départ");
                if (_number != value)
                {
                    _number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        private int _draftMask;
        public int DraftMask
        {
            get
            {
                return _draftMask;
            }
            set
            {
                if (_draftMask != value)
                {
                    _draftMask = value;
                    OnPropertyChanged("DraftMask");
                }
            }
        }

        public bool IsStartCell()
        {
            return _startCell > 0;
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
