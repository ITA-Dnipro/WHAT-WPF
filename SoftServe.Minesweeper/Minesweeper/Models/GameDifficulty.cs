using Minesweeper.Enums;

namespace Minesweeper.Models
{
    public class GameDifficulty : Base
    {
        private readonly string title;
        private GameSettings difficulty;
        private int gameFieldSize;

        public string Title => title;

        public GameSettings Difficulty
        {
            get => difficulty;
            set
            {
                difficulty = value;
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

        public GameDifficulty(GameSettings settings, int gameFieldSize)
        {
            title = settings.ToString();

            Difficulty = settings;

            GameFieldSize = gameFieldSize;
        }
    }
}
