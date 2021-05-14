using System.ComponentModel;
using System.Runtime.CompilerServices;
using _2048.Enums;

namespace _2048.Models
{
	public class Cell : INotifyPropertyChanged //TODO: Rename, Replace, OnPropertyChangedClass
	{
		private CellValue _value = 0;
		private bool _isSum = false;

		public int RowPosition;
		public int ColumnPosition;

		public CellValue Value
		{
			get { return _value; }
			set
			{
				_value = value;
				OnPropertyChanged();
			}
		}

		public bool IsSum
		{
			get { return _isSum; }
			set
			{
				_isSum = value;
				OnPropertyChanged();
			}
		}

		public Cell(int rowPosition, int columnPosition)
		{
			RowPosition = rowPosition;
			ColumnPosition = columnPosition;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
