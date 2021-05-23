using System.Collections.Generic;
using _2048.View;
using _2048.Enums;


namespace _2048.Models
{
	public class Board
	{
		public int length = 4;
		public int width = 4;

		public Cell[,] cells;
		public List<Cell> cellsList;
		public List<Cell> freeCellsList;

		public Board()
		{
			cells = new Cell [length, width];
			cellsList = new List<Cell>();

			for (int rowPosition = 0; rowPosition < length; rowPosition++)
            {
				for (int columnPosition = 0; columnPosition < width; columnPosition++)
				{
					cells[rowPosition, columnPosition] = new Cell(rowPosition, columnPosition);
					cellsList.Add(cells[rowPosition, columnPosition]);
				}
            }
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
