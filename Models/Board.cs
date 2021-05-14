using System.Collections.Generic;

namespace _2048.Models
{
	public class Board
	{
		public Cell[,] cells;
		public List<Cell> cellsList = new List<Cell>();

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
		}
	}
}
