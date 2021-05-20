using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using _2048.Enums;
using _2048.View;


namespace _2048.Services
{
	public class CellValueGenerator
    {
		public void Generate (List<Cell> freeCellsList, int freeCellsCounter)
        {
			Random _random = new Random();
            int randomIndex = _random.Next(freeCellsCounter);

            if (_random.NextDouble() > 0.1)
            {
                freeCellsList[randomIndex].Value = CellValue.One;
            }
            else
            {
                freeCellsList[randomIndex].Value = CellValue.Two;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
