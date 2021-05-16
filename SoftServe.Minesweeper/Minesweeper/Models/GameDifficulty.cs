using Minesweeper.Enums;

namespace Minesweeper.Models
{
    public class GameDifficulty : Base
    {
        private readonly string title;
        private GameSettings difficulty;

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

        public GameDifficulty(GameSettings settings)
        {
            title = settings.ToString();

            Difficulty = settings;
        }
    }
}
