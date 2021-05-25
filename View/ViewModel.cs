using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using _2048.Services;
using _2048.Utils;
using _2048.Enums;

namespace _2048.View
{
	public class ViewModel : OnPropertyChangedClass 
	{
		private CellValueMover _mover;

		public List<Cell> Cells
		{
			get
			{
				return _mover.board.cellsList;
			}
			set
			{
				OnPropertyChanged();
			}
		}

		public bool IsGameOver
		{
			get
			{
				return _mover.IsGameOver;
			}
			private set
			{
				OnPropertyChanged();
			}
		}

		public int BoardLength
		{
			get
			{
				return _mover.board.length;
			}
		}

		public int BoardWidth
		{
			get
			{
				return _mover.board.width;
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
			nameof(CellValueCalculator.Score),
			nameof(CellValueMover.IsGameOver),
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
				_mover.PropertyChanged -= Model_PropertyChanged;
				_mover.calculator.PropertyChanged -= Model_PropertyChanged;
			}

			_mover = new CellValueMover();
			Cells = _mover.board.cellsList;
			IsGameOver = false;
			Score = 0;

			_mover.PropertyChanged += Model_PropertyChanged;
			_mover.calculator.PropertyChanged += Model_PropertyChanged;

			_mover.EndStep();
			_mover.EndStep();
		}

		public void NextStep(string control)
		{
			if (IsGameOver)
			{
				return;
			}

			switch (control)
			{
				case "Left":
					_mover.PrepareStep(MoveDirection.Left);
					break;
				case "Right":
					_mover.PrepareStep(MoveDirection.Right);
					break;
				case "Up":
					_mover.PrepareStep(MoveDirection.Up);
					break;
				case "Down":
					_mover.PrepareStep(MoveDirection.Down);
					break;
				case "End":
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
			_mover.IsGameOver = true;
			OnPropertyChanged("Cells");
		}
	}
}
