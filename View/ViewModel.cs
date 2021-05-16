using System.Collections.Generic;
using _2048.Services;
using _2048.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using _2048.Enums;

namespace _2048.View
{
	public class ViewModel : INotifyPropertyChanged //TODO: OnPropertyChangedClass
	{
		private CellValueGenerator _generator;
		private CellValueMover _mover;
		private List<Cell> _cells;

		public List<Cell> Cells
		{
			get
			{
				return _cells;
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
				return _generator.IsGameOver;
			}
		}

		public int Score
		{
			get
			{
				return _mover.calculator.Score;
			}
		}

		List<string> Properties = new List<string>() 
		{ 
			nameof(CellValueCalculator.Score) 
		};

		public ViewModel()
		{
			Initialize();
		}

		public void Initialize()
		{
			_mover = new CellValueMover();
			_generator = new CellValueGenerator(_mover.board);
			Cells = _mover.board.cellsList;
			_mover.calculator.PropertyChanged += Model_PropertyChanged;

			new CellValueGenerator(_mover.board);
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

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
