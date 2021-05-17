using Minesweeper.Enums;
using Minesweeper.Interfaces;
using Minesweeper.Models;
using Minesweeper.Models.Helpers;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Minesweeper.ViewModels
{
    public class GameViewModel : Base
    {
        private List<List<Cell>> gameField;
        private ICommand cellClickCommand;
        private ICommand cellRightClickCommand;
        private ICommand newGameCommand;
        private ICommand settingsCommand;
        private ICommand closeCommand;
        private int flagsOnField = 0;
        private ObservableCollection<GameDifficulty> gameDifficulties = new ObservableCollection<GameDifficulty>();
        private GameDifficulty difficulty;
        private int gameFieldSize = 10;
        private bool isWin = true;

        public bool IsWin
        {
            get => isWin;
            set
            {
                isWin = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GameDifficulty> GameDifficulties
        {
            get => gameDifficulties;
            set
            {
                gameDifficulties = value;
                OnPropertyChanged();
            }
        }

        public int GameFieldSize
        {
            get => gameFieldSize;
            set
            {
                gameFieldSize = value;
                OnPropertyChanged();
            }
        }

        public GameDifficulty Difficulty
        {
            get => difficulty;
            set
            {
                difficulty = value;
                OnPropertyChanged();
            }
        }

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



                if (GameWin())
                {
                    IsWin = true;
                }
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
                    if (FlagsOnField == GameFieldSize || !cell.IsHidden)
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

        public ICommand SettingsCommand => settingsCommand ?? new RelayParameterCommand(parameter =>
        {
            if (parameter is GameDifficulty gameDifficulty)
            {
                GameFieldSize = gameDifficulty.GameFieldSize;

                NewGame();
            }
        });

        public ICommand CloseCommand => closeCommand ?? new RelayCommand(() => { this.OnClosingRequest(); });

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

            GameField = gameFieldCreator.CreateGameField(GameFieldSize);

            FlagsOnField = 0;

            IsWin = false;
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

        private bool GameWin()
        {
            int numOpenCells = 0;

            for (int i = 0; i < GameFieldSize; i++)
            {
                List<Cell> row = GameField[i];

                for (int j = 0; j < GameFieldSize; j++)
                {
                    if (!row[j].IsHidden)
                    {
                        numOpenCells++;
                    }
                    
                }
            }

            return numOpenCells == GameFieldSize * GameFieldSize - GameFieldSize;
        }

        public GameViewModel()
        {
            GameDifficulties.Add(new GameDifficulty(GameSettings.Easy, 10));
            GameDifficulties.Add(new GameDifficulty(GameSettings.Medium, 15));
            GameDifficulties.Add(new GameDifficulty(GameSettings.Hard, 18));

            Difficulty = GameDifficulties[0];

            NewGame();
        }
    }
}
