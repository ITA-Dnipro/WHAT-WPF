﻿using System.Collections.Generic;
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

		public ViewModel()
		{
			Initialize();
		}

		public void Initialize()
		{
			_mover = new CellValueMover();
			_generator = new CellValueGenerator(_mover.board);
			Cells = _mover.board.cellsList;
			new CellValueGenerator(_mover.board);
		}

		public void NextStep(MoveDirection direction)
		{
			if (IsGameOver)
			{
				return;
			}

			_mover.Step(direction);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
