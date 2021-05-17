using Minesweeper.Enums;
using Minesweeper.Interfaces;
using System;
using System.Collections.Generic;

namespace Minesweeper.Models.Helpers
{
    public class GameFieldCreator : IGameFieldCreator
    {
        int gameFieldSize = 0;
        int numOfBombs = 0;

        List<List<Cell>> gameField = new List<List<Cell>>();

        public List<List<Cell>> CreateGameField(int gameFieldSize)
        {
            this.gameFieldSize = gameFieldSize;

            int x = 0;

            for (int i = 0; i < gameFieldSize; i++)
            {
                int y = 0;

                List<Cell> row = new List<Cell>();

                if (i % 2 == 0)
                {
                    for (int j = 0; j < gameFieldSize; j++)
                    {
                        Cell cell = new Cell(j % 2 == 0 ? HiddenCellColor.Color1 : HiddenCellColor.Color2, x, y);
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
                        Cell cell = new Cell(j % 2 == 0 ? HiddenCellColor.Color2 : HiddenCellColor.Color1, x, y);
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

            MinedField();

            FillGameField();

            return gameField;
        }

        public void FillGameField()
        {
            for (int i = 0; i < gameField.Count; i++)
            {
                List<Cell> row = gameField[i];

                for (int j = 0; j < gameField.Count; j++)
                {
                    if (row[j].IsMined)
                    {
                        continue;
                    }
                    else
                    {
                        row[j].OpenContent = HowMuchBombsNear(gameField, i, j);
                    }
                }
            }
        }

        public void MinedField()
        {
            Random rand = new Random();

            for (int i = 0; i < gameFieldSize; i++)
            {
                List<Cell> row = gameField[i];

                for (int j = 0; j < gameFieldSize; j++)
                {
                    if (row[j].IsMined)
                    {
                        continue;
                    }

                    row[j].IsMined = rand.Next(0, gameFieldSize) % 11 == 1 && numOfBombs < gameFieldSize ? true : false;
                    if (row[j].IsMined)
                    {
                        numOfBombs++;

                    }
                }
            }

            if (numOfBombs == gameFieldSize)
            {
                return;
            }
            else
            {
                MinedField();
            }
        }


        public ContentInCell HowMuchBombsNear(List<List<Cell>> gameField, int cellRow, int cellCol)
        {
            int bombsNear = 0;

            for (int i = cellRow - 1; i <= cellRow + 1; i++)
            {
                if (i < 0 || i > gameFieldSize - 1)
                {
                    continue;
                }
                List<Cell> row = gameField[i];

                for (int j = cellCol - 1; j <= cellCol + 1; j++)
                {
                    if (j < 0 || j > gameFieldSize - 1)
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
