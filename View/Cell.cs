using System.ComponentModel;
using System.Runtime.CompilerServices;
using _2048.Enums;
using _2048.Utils;


namespace _2048.View
{
	public class Cell : OnPropertyChangedClass
	{
		private CellValue _value = 0;
		private bool _isSum = false;

		public int RowPosition;
		public int ColumnPosition;

		public CellValue Value
		{
			get
			{ 
				return _value; 
			}
			set
			{
				_value = value;
				OnPropertyChanged();
			}
		}

		public bool IsSum
		{
			get 
			{ 
				return _isSum; 
			}
			set
			{
				_isSum = value;
			}
		}

		public Cell(int rowPosition, int columnPosition)
		{
			RowPosition = rowPosition;
			ColumnPosition = columnPosition;
		}
	}
}
