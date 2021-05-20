using _2048.Enums;
using _2048.Models;
using _2048.View;
using _2048.Utils;


namespace _2048.Services
{
    public class CellValueMover
    {
        public Board board = new Board();
        public CellValueGenerator generator = new CellValueGenerator();
        public CellValueCalculator calculator = new CellValueCalculator();
        public Validator validator = new Validator();

        public void Step(MoveDirection direction)
        {
            calculator.isMoved = false;

            foreach (Cell cell in board.cellsList)
            {
                cell.IsSum = false;
            }

            switch (direction)
            {
                case MoveDirection.Down:
                    Down();
                    break;
                case MoveDirection.Left:
                    Left();
                    break;
                case MoveDirection.Right:
                    Right();
                    break;
                case MoveDirection.Up:
                    Up();
                    break;
            }

            if (calculator.isMoved)
            {
                generator.Generate(board.GetFreeCells(), board.GetFreeCells().Count);
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

            while (validator.IsCellInBoardRange(currentCell, targetRow, targetCol))
            {
                currentCell = calculator.Calculate(currentCell,
                        board.cells[currentCell.RowPosition + targetRow, currentCell.ColumnPosition + targetCol]);
            }
        }
    }
}
