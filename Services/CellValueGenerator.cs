using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using _2048.Enums;
using _2048.Models;

namespace _2048.Services
{
	public class CellValueGenerator : INotifyPropertyChanged //TODO: Rename? Replace? OnPropertyChangedClass
    {
        private bool _isGameOver;
        private int _freeCellsCounter;

        public List<Cell> freeCells;
        Random random;

        public bool IsGameOver
        {
            get 
            { 
                return _isGameOver; 
            }
            set
            {
                _isGameOver = value;
                OnPropertyChanged();
            }
        }

        public int FreeCellsCounter
        {
            get
            {
                return _freeCellsCounter;
            }
            set
            {
                _freeCellsCounter = value;
                OnPropertyChanged();
            }
        }

        public CellValueGenerator(Board board)
        {
            _isGameOver = false;

            random = new Random();
            freeCells = new List<Cell>();

            foreach (Cell cell in board.cells)
            {
                if (cell.Value == 0)
                {
                    freeCells.Add(cell);
                }
            }

            if (freeCells.Count == 0)
            {
                IsGameOver = true;
            }
            else
            {
                int randomIndex = random.Next(freeCells.Count());

                if (random.NextDouble() > 0.1)
                {
                    freeCells[randomIndex].Value = CellValue.One;
                }
                else
                {
                    freeCells[randomIndex].Value = CellValue.Two;
                }

                freeCells.Remove(freeCells[randomIndex]);

                FreeCellsCounter = freeCells.Count() - 1;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
