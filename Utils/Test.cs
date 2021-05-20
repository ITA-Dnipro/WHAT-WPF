using System;
using System.Collections.Generic;
using _2048.View;
using _2048.Enums;

namespace _2048.Utils
{
	public class Test
	{
		public List<Cell> GetTestCells()
		{
			List<Cell> testCells = new List<Cell>();
			for (int i = 1; i <= 16; i++)
			{
				int value = (int)Math.Pow(2, i);
				testCells.Add(new Cell(0, 0) { Value = (CellValue)value } );
			}

			return testCells;
		}
	}
}
