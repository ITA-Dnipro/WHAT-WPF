using System.Collections.Generic;
using _2048.View;
using _2048.Enums;


namespace _2048.Models
{
	public class Board
	{
		public Cell[,] cells;
		public List<Cell> cellsList = new List<Cell>();
		public List<Cell> freeCellsList;

		public Board()
		{
			cells = new Cell [4,4];

            for (int rowPosition = 0; rowPosition < 4; rowPosition++)
            {
				for (int columnPosition = 0; columnPosition < 4; columnPosition++)
				{
					cells[rowPosition, columnPosition] = new Cell(rowPosition, columnPosition);
					cellsList.Add(cells[rowPosition, columnPosition]);
				}
            }

			//GetFreeCells();
		}

		public List<Cell> GetFreeCells()
		{
			freeCellsList = new List<Cell>();

			foreach (Cell cell in cells)
			{
				if (cell.Value == CellValue.None)
				{
					freeCellsList.Add(cell);
				}
			}
			return freeCellsList;
		}
	}
}
