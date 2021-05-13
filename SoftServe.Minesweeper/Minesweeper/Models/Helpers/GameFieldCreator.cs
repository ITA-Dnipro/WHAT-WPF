using Minesweeper.Enums;
using Minesweeper.Interfaces;
using System;
using System.Collections.Generic;

namespace Minesweeper.Models.Helpers
{
    public class GameFieldCreator : IGameFieldCreator
    {
        public List<List<Cell>> CreateGameField(int gameFieldSize)
        {
            List<List<Cell>> gameField = new List<List<Cell>>();

            Random rand = new Random();

            int numOfBombs = 0;

            int x = 0;

            for (int i = 0; i < gameFieldSize; i++)
            {
                int y = 0;

                List<Cell> row = new List<Cell>();

                if (i % 2 == 0)
                {
                    for (int j = 0; j < gameFieldSize; j++)
                    {
                        Cell cell = new Cell(rand.Next(0, gameFieldSize) % 11 == 1 && numOfBombs < gameFieldSize ? true : false, j % 2 == 0 ? HiddenCellColor.Color1 : HiddenCellColor.Color2, x, y);
                        row.Add(cell);

                        if (cell.IsMined == true)
                        {
                            numOfBombs++;
                        }

                        y++;
                    }
                }
                else
                {
                    for (int j = 0; j < gameFieldSize; j++)
                    {
                        Cell cell = new Cell(rand.Next(0, gameFieldSize) % 11 == 1 && numOfBombs < gameFieldSize ? true : false, j % 2 == 0 ? HiddenCellColor.Color2 : HiddenCellColor.Color1, x, y);
                        row.Add(cell);

                        if (cell.IsMined == true)
                        {
                            numOfBombs++;
                        }

                        y++;
                    }
                }
                gameField.Add(row);

                x++;
            }

            FillGameField(gameField);

            return gameField;
        }

        public void FillGameField(List<List<Cell>> gamefiled)
        {
            for (int i = 0; i < gamefiled.Count; i++)
            {
                List<Cell> row = gamefiled[i];

                for (int j = 0; j < gamefiled.Count; j++)
                {
                    if (row[j].IsMined)
                    {
                        continue;
                    }
                    else
                    {
                        row[j].OpenContent = HowMuchBombsNear(gamefiled, i, j);
                    }
                }
            }
        }

        public ContentInCell HowMuchBombsNear(List<List<Cell>> gameField, int cellRow, int cellCol)
        {
            int bombsNear = 0;

            for (int i = cellRow - 1; i <= cellRow + 1; i++)
            {
                if (i < 0 || i > 9)
                {
                    continue;
                }
                List<Cell> row = gameField[i];

                for (int j = cellCol - 1; j <= cellCol + 1; j++)
                {
                    if (j < 0 || j > 9)
                    {
                        continue;
                    }
                    if (i == cellRow && j == cellCol)
                    {
                        continue;
                    }
                    else if (row[j].IsMined)
                    {
                        bombsNear++;
                    }
                }
            }

            return (ContentInCell)bombsNear;
        }
    }
}
