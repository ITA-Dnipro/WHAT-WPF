using System;
using _2048.Enums;
using _2048.Models;

namespace _2048.Services
{
	public class CellValueMover
	{
        public Board board = new Board();
        public CellValueCalculator calculator = new CellValueCalculator();

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

                new CellValueGenerator(board);
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
        }

        public void Move(int rowPosition, int columnPosition, MoveDirection direction)
        {
            if (rowPosition < 0             //TODO: Method
                    || rowPosition > 3 
                    || columnPosition < 0 
                    || columnPosition > 3)
            {
                throw new Exception("Exception in Move"); //TODO: Handle
            }

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

            while (currentCell != null  //TODO: Method
                    && currentCell.RowPosition + targetRow >= 0
                    && currentCell.RowPosition + targetRow <= 3
                    && currentCell.ColumnPosition + targetCol >= 0
                    && currentCell.ColumnPosition + targetCol <= 3)
            {
                currentCell = calculator.Calculate(currentCell,
                        board.cells[currentCell.RowPosition + targetRow, currentCell.ColumnPosition + targetCol]);
            }
        }
    }
}
