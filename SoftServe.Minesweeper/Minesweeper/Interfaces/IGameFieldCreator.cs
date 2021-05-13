using Minesweeper.Enums;
using Minesweeper.Models;
using System.Collections.Generic;

namespace Minesweeper.Interfaces
{
    public interface IGameFieldCreator
    {
        List<List<Cell>> CreateGameField(int gameFieldSize);

        void FillGameField(List<List<Cell>> gamefiled);

        ContentInCell HowMuchBombsNear(List<List<Cell>> gameField, int cellRow, int cellCol);
    }
}
