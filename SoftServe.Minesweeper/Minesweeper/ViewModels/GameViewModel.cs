using Minesweeper.Interfaces;
using Minesweeper.Models;
using Minesweeper.Models.Helpers;
using System.Collections.Generic;
using System.Windows.Input;

namespace Minesweeper.ViewModels
{
    public class GameViewModel : NotifyPropertyChanged
    {
        private List<List<Cell>> gameField;
        private ICommand cellClickCommand;
        private ICommand cellRightClickCommand;
        private ICommand newGameCommand;
        private int flagsOnField = 0;

        public List<List<Cell>> GameField
        {
            get => gameField;
            set
            {
                gameField = value;
                OnPropertyChanged();
            }
        }

        public ICommand CellClickCommand => cellClickCommand ?? new RelayParameterCommand(parameter =>
        {
            if (parameter is Cell cell)
            {
                if (!cell.IsMined)
                {
                    int flagsOnFieldTemp = FlagsOnField;

                    CellsOpener cellsOpener = new CellsOpener();


                    cellsOpener.OpenCells(GameField, cell.X, cell.Y, ref flagsOnFieldTemp);

                    FlagsOnField = flagsOnFieldTemp;
                }
                else
                {
                    GameOver();
                }
            }
        }, parameter => parameter is Cell cell);

        public ICommand CellRightClickCommand => cellRightClickCommand ?? new RelayParameterCommand(parameter =>
        {
            if (parameter is Cell cell)
            {
                
                if (!cell.IsFlaged)
                {
                    if (FlagsOnField == 10 || !cell.IsHidden)
                    {
                        return;
                    }

                    cell.IsFlaged = true;
                    FlagsOnField++;
                }
                else
                {
                    cell.IsFlaged = false;
                    FlagsOnField--;
                }
            }
        }, parameter => parameter is Cell cell);

        public ICommand NewGameCommand => newGameCommand ?? new RelayCommand(() => NewGame());

        public int FlagsOnField
        {
            get => flagsOnField;
            set
            {
                flagsOnField = value;
                OnPropertyChanged();
            }
        }

        private void NewGame()
        {
            IGameFieldCreator gameFieldCreator = new GameFieldCreator();

            GameField = gameFieldCreator.CreateGameField(10);

            FlagsOnField = 0;
        }

        private void GameOver()
        {
            for (int i = 0; i < gameField.Count; i++)
            {
                List<Cell> row = GameField[i];

                for (int j = 0; j < gameField.Count; j++)
                {
                    if (!row[j].IsHidden)
                    {
                        continue;
                    }

                    if (!row[j].IsFlaged)
                    {
                        row[j].IsHidden = false;
                    }
                }
            }
        }

        public GameViewModel()
        {
            NewGame();
        }
    }
}
