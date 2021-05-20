using System;
using _2048.Enums;
using _2048.View;
using _2048.Utils;



namespace _2048.Services
{
	public class CellValueCalculator : OnPropertyChangedClass
	{
		private int _score;

		public bool isMoved = true;

		public int Score 
		{
			get
			{
				return _score;
			}
			set
			{
				_score = value;
				OnPropertyChanged();
			}
		}

		public Cell Calculate(Cell currentCell, Cell targetCell)
		{
			if (!IsCanCalculate(currentCell, targetCell))
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

				targetCell.Value = (CellValue)sumValue;
				currentCell.Value = 0;

				targetCell.IsSum = true;
				currentCell.IsSum = false;

				isMoved = true;
				Score += sumValue;

				return targetCell;
			}

			throw new Exception("Exception in Calculate");
		}

		private bool IsCanCalculate(Cell currentCell, Cell targetCell)
		{
			if (currentCell == null             
					|| targetCell == null
					|| currentCell.Value == 0
					|| currentCell.IsSum == true
					|| targetCell.IsSum == true
					|| (targetCell.Value != 0 && targetCell.Value != currentCell.Value))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
