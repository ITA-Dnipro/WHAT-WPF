using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tetris.Model.Shape;

namespace Tetris.Model
{
    class GameManager
    {
        public FieldFiller Filler { get; } = new FieldFiller();
        public ShapeCreator FigureCreator { get; } = new ShapeCreator();


        public const int COLUMNS = 10;
        public const int ROWS = 20;

        private bool _isEndOfGame;
        private List<Coordinate> _listOfAllPoints;
       // private BaseShape _movingShape;

        public bool IsEndOfGame => _isEndOfGame;
        public List<Coordinate> GetAllPoints => _listOfAllPoints;
        public BaseShape MovingShape { get; set; }
    }
}
