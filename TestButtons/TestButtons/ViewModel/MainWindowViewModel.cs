using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TestButtons.Model;

namespace TestButtons.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private int _quantity;
        private bool isGameOver = false;
        private int xWin = 0;
        private int oWin = 0;
        private int amountClickng = 0;
        private Game game;
        private string _winner;
        private string _forLblX;
        private string _forLblO;
        private string _visibility;
        private bool _enableField;

        public List<string> ComboItems { get; }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
            }
        }

        public string Winner
        {
            get => _winner;
            set
            {
                _winner = value;
                OnPropertyChanged(nameof(Winner));
            }
        }

        public string ForLblX
        {
            get => _forLblX;
            set
            {
                _forLblX = value;
                OnPropertyChanged(nameof(ForLblX));
            }
        }

        public string ForLblO
        {
            get => _forLblO;
            set
            {
                _forLblO = value;
                OnPropertyChanged(nameof(ForLblO));
            }
        }

        public string Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public bool EnableField 
        {
            get => _enableField;
            set
            {
                _enableField = value;
                OnPropertyChanged(nameof(EnableField));
            }
        }

        public ObservableCollection<CellViewModel> NewField { get; }

        public MainWindowViewModel()
        {
            ComboItems = new List<string>();

            for (int i = 3; i <= 10; i++)
            {
                ComboItems.Add(i.ToString());
            }

            Quantity = 3;
            NewField = new ObservableCollection<CellViewModel>();
            InitializeCells();
            game = new Game(Quantity);
            ForLblX = "X: 0";
            ForLblO = "O: 0";
            Visibility = "Hidden";
            EnableField = true;
        }

        private ObservableCollection<CellViewModel> InitializeCells()
        {
            
            for (int i = 0; i < Quantity; i++)
            {
                for (int j = 0; j < Quantity; j++)
                {
                    NewField.Add(new CellViewModel()
                    {
                        Row = i,
                        Column = j
                    });
                }
            }

            return NewField;
        }

       private void ChangeParametrsForGameOver(string winner)
        {
            Winner = winner;
            isGameOver = true;

            Visibility = "Visible";
            EnableField = false;
        }

        private void CheckOnWinnig()
        {
            var (isWin, winner) = game.CheckResults();

            if (isWin)
            {
                if (winner == MarkType.X.ToString())
                {
                    ForLblX = $"X:{++xWin}";
                    ChangeParametrsForGameOver("Player X is winner!");
                }
                if (winner == MarkType.O.ToString())
                {
                    ForLblO = $"O:{++oWin}";
                    ChangeParametrsForGameOver("Player O is winner!");
                }
            }
            if (isWin == false && amountClickng == Quantity * Quantity)
            {
                ChangeParametrsForGameOver("Nobody win!");
            }
        }


        private ICommand _checkWinning;

        public ICommand CheckWinning
        {
            get
            {
                if (_checkWinning == null)
                {
                    _checkWinning = new RelayCommand(
                        param => ((CellViewModel)param).MarkTypes == null,
                        param =>
                        {
                            ((CellViewModel)param).GetMarkType();
                            game[((CellViewModel)param).Row, ((CellViewModel)param).Column] = ((CellViewModel)param).MarkTypes;
                            amountClickng++;
                            CheckOnWinnig();
                        }
                    );
                }
                return _checkWinning;
            }
        }

        private ICommand _startNewGame;

        public ICommand StartNewGame
        {
            get
            {
                if (_startNewGame == null)
                {
                    _startNewGame = new RelayCommand(
                        param => this.CanExecute(),
                        param => this.Execute()
                    ); 
                }
               
                return _startNewGame;
            }
        }

        private ICommand _refresh;

        public ICommand Refresh
        {
            get
            {
                if (_refresh == null)
                {
                    _refresh = new RelayCommand(
                        param => isGameOver == false,
                        param => this.Refreshing()
                    );
                }

                return _refresh;
            }
        }

        private bool CanExecute()
        {
            if (isGameOver == false && CheckOnEmptyCollection() == false && NewField.Count == Quantity*Quantity)
            {
                return false;
            }
            return true;
        }

        private bool CheckOnEmptyCollection()
        {
            int countEmptys = 0;
            foreach(CellViewModel cell in NewField)
            {
                if(cell.MarkTypes == null)
                {
                    countEmptys++;
                }
            }

            if(countEmptys==Quantity*Quantity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Refreshing()
        {
            if (isGameOver == false && CheckOnEmptyCollection() == false && NewField.Count == Quantity * Quantity)
            {
                if (MessageBox.Show("Do you want to cancel the game?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    game.ClearMarks();
                    amountClickng = 0;
                    CellViewModel.SetIsX(true);

                    foreach (CellViewModel cell in NewField)
                    {
                        cell.ClearMarkType();
                    }
                }
            }

            OnPropertyChanged(nameof(NewField));
        }

        private void Execute()
        {
            isGameOver = false;
            Visibility = "Hidden";
            EnableField = true;
            NewField.Clear();
            CellViewModel.SetIsX(true);
            game = new Game(Quantity);
            amountClickng = 0;

            for (int i = 0; i < Quantity; i++)
            {
                for (int j = 0; j < Quantity; j++)
                {
                    NewField.Add(new CellViewModel()
                    {
                        Row = i,
                        Column = j
                    });
                }
            }

            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(NewField));
        }
        


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
