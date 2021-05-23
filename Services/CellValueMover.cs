using System.Collections.Generic;
using _2048.Enums;
using _2048.Models;
using _2048.View;
using _2048.Utils;

namespace _2048.Services
{
    public class CellValueMover : OnPropertyChangedClass
    {
        bool _isStepCheckMode;
        bool _isStepPossible;
        bool _isGameOver;

        public Board board = new Board();
        public int freeCellsCounter;

        public CellValueGenerator generator = new CellValueGenerator();
        public CellValueCalculator calculator = new CellValueCalculator();
        public Validator validator = new Validator();
        public List<Cell> freeCellsList;

		public bool IsGameOver
        {
            get
            {
                return _isGameOver;
            }
            set
            {
                _isGameOver = value;
                OnPropertyChanged();
            }
        }
        

        public void PrepareStep(MoveDirection direction)
        {
            _isStepCheckMode = false;
            _isStepPossible = true;

			if (_isStepPossible == true)
            {
                calculator.isMoved = false;

                foreach (Cell cell in board.cellsList)
                {
                    cell.IsSum = false;
                }

                Step(direction);
            }
        }

        private void CheckStepPossibility()
        {
            _isStepPossible = false;

            for (int direction = 1; direction <= 4 && _isStepCheckMode == true; direction++)
            {
                Step((MoveDirection)direction);
            }

            _isStepCheckMode = false;
            IsGameOver = !_isStepPossible;
        }

        public void Step(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    Left();
                    break;
                case MoveDirection.Right:
                    Right();
                    break;
                case MoveDirection.Up:
                    Up();
                    break;
                case MoveDirection.Down:
                    Down();
                    break;
            }

            if (_isStepCheckMode == false && calculator.isMoved)
            {
                EndStep();
            }
        }

        public void EndStep()
        {
            freeCellsList = board.GetFreeCells();
            freeCellsCounter = freeCellsList.Count;
            if (freeCellsCounter > 0)
            {
                generator.Generate(freeCellsList, freeCellsCounter);
                --freeCellsCounter;
            }
			if (freeCellsCounter <= 0)
			{
				_isStepCheckMode = true;
				CheckStepPossibility();
			}
		}

        public void Up()
        {
            for (int rowPosition = 1; rowPosition <= 3; rowPosition++)
            {
                for (int columnPosition = 0; columnPosition <= 3; columnPosition++)
                {
                    Move(rowPosition, columnPosition, MoveDirection.Up);
                }
            }

            return;
        }

        public void Down()
        {
            for (int rowPosition = 2; rowPosition >= 0; rowPosition--)
            {
                for (int columnPosition = 0; columnPosition <= 3; columnPosition++)
                {
                    Move(rowPosition, columnPosition, MoveDirection.Down);
                }
            }

            return;
        }

        public void Left()
        {
            for (int columnPosition = 1; columnPosition <= 3; columnPosition++)
            {
                for (int rowPosition = 0; rowPosition <= 3; rowPosition++)
                {
                    Move(rowPosition, columnPosition, MoveDirection.Left);
                }
            }

            return;
        }

        public void Right()
        {
            for (int columnPosition = 2; columnPosition >= 0; columnPosition--)
            {
                for (int rowPosition = 0; rowPosition <= 3; rowPosition++)
                {
                    Move(rowPosition, columnPosition, MoveDirection.Right);
                }
            }

            return;
        }

        public void Move(int rowPosition, int columnPosition, MoveDirection direction)
        {
            Cell currentCell = board.cells[rowPosition, columnPosition];

            int targetRow = 0;
            int targetCol = 0;

            switch (direction)
            {
                case MoveDirection.Left:
                    targetCol = -1;
                    break;
                case MoveDirection.Right:
                    targetCol = +1;
                    break;
                case MoveDirection.Up:
                    targetRow = -1;
                    break;
                case MoveDirection.Down:
                    targetRow = +1;
                    break;
            }

            if (_isStepCheckMode == true)
            {
                while (validator.IsCellInBoardRange(currentCell, targetRow, targetCol) && _isStepPossible == false)
                {
                    if (currentCell.Value == board.cells[currentCell.RowPosition + targetRow, currentCell.ColumnPosition + targetCol].Value)
                    {
                        _isStepPossible = true;
                        break;
                    }

                    currentCell = board.cells[currentCell.RowPosition + targetRow, currentCell.ColumnPosition + targetCol];
                }
            }

            else
            {
                while (validator.IsCellInBoardRange(currentCell, targetRow, targetCol))
                {
                    currentCell = calculator.Calculate(currentCell,
                            board.cells[currentCell.RowPosition + targetRow, currentCell.ColumnPosition + targetCol]);
                }
            }
        }
    }
}
