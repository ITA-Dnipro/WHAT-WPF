using System.Collections.Generic;
using _2048.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using _2048.Enums;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using _2048.Utils;

namespace _2048.View
{
	public class ViewModel : OnPropertyChangedClass 
	{
		private bool _isGameOver;
		private CellValueMover _mover;
		private List<Cell> _cells;

		public List<Cell> Cells
		{
			get
			{
				return _mover.board.cellsList;
			}
			set
			{
				_cells = value;
				OnPropertyChanged();
			}
		}

		public bool IsGameOver
		{
			get
			{
				return _isGameOver;
			}
			private set
			{
				_isGameOver = value;
				OnPropertyChanged();
			}
		}

		public int Score
		{
			get
			{
				return _mover.calculator.Score;
			}
			private set
			{
				OnPropertyChanged();
			}
		}

		List<string> Properties = new List<string>() 
		{ 
			nameof(CellValueCalculator.Score)
		};

		public ICommand NewGameCommand
		{
			get
			{
				return new RelayCommand(() => Initialize());
			}
		}

		public ViewModel()
		{
			Initialize();
		}

		private void Initialize()
		{
			if (_mover != null)
			{
				_mover.calculator.PropertyChanged -= Model_PropertyChanged;
			}

			_mover = new CellValueMover();
			Cells = _mover.board.cellsList;
			IsGameOver = false;
			Score = 0;

			_mover.calculator.PropertyChanged += Model_PropertyChanged;

			_mover.generator.Generate(_mover.board.GetFreeCells(), _mover.board.GetFreeCells().Count);
			_mover.generator.Generate(_mover.board.GetFreeCells(), _mover.board.GetFreeCells().Count);

		}

		public void NextStep(string control)
		{
			if (IsGameOver)
			{
				return;
			}

			switch (control)
			{
				case "Up":
					_mover.Step(MoveDirection.Up);
					break;
				case "Down":
					_mover.Step(MoveDirection.Down);
					break;
				case "Left":
					_mover.Step(MoveDirection.Left);
					break;
				case "Right":
					_mover.Step(MoveDirection.Right);
					break;
				case "Space":
					TestMode();
					break;
			}
		}

		private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			string propertyName = e.PropertyName;
			if (string.IsNullOrEmpty(propertyName) || Properties.IndexOf(propertyName) >= 0)
			{
				OnPropertyChanged(propertyName);
			}
		}

		private void TestMode()
		{
			_mover.board.cellsList = new Test().GetTestCells();
			IsGameOver = true;
			OnPropertyChanged("Cells");
		}
	}
}
