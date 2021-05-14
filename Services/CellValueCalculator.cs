using System;
using _2048.Models;
using _2048.Enums;

namespace _2048.Services
{
	public class CellValueCalculator
	{
		public bool isMoved = true;

		public Cell Calculate(Cell currentCell, Cell targetCell)
		{
			if (currentCell == null				//TODO: Method
					|| targetCell == null
					|| currentCell.Value == 0
					|| currentCell.IsSum == true
					|| targetCell.IsSum == true
					|| (targetCell.Value != 0 && targetCell.Value != currentCell.Value))
			{
				return null;
			}

			if (targetCell.Value == 0)
			{
				targetCell.Value = currentCell.Value;
				currentCell.Value = 0;

				targetCell.IsSum = false;
				currentCell.IsSum = false;
				isMoved = true;

				return targetCell;
			}

			if (targetCell.Value == currentCell.Value)
			{
				int sumValue = (int)targetCell.Value + (int)currentCell.Value;

				if (!Enum.IsDefined(typeof(CellValue), sumValue))
				{
					throw new Exception("Exception in sumValue"); //TODO: Handle
				}

				targetCell.Value = (CellValue)sumValue;
				currentCell.Value = 0;

				targetCell.IsSum = true;
				currentCell.IsSum = false;
				isMoved = true;

				return targetCell;
			}

			throw new Exception("Exception in CellValueMover"); //TODO: Handle
		}
	}
}
