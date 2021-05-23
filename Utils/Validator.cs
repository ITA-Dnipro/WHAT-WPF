using _2048.View;

namespace _2048.Utils
{
	public class Validator
	{
		public bool IsCellInBoardRange(Cell currentCell, int rowIndexerCorrector, int ColumnIndexerCorrector)
		{
			if (currentCell != null
					&& currentCell.RowPosition + rowIndexerCorrector >= 0
					&& currentCell.RowPosition + rowIndexerCorrector <= 3
					&& currentCell.ColumnPosition + ColumnIndexerCorrector >= 0
					&& currentCell.ColumnPosition + ColumnIndexerCorrector <= 3)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
