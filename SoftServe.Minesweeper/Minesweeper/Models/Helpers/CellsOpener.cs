using System.Collections.Generic;

using Minesweeper.Enums;

namespace Minesweeper.Models.Helpers
{
    public class CellsOpener
    {
        public int OpenCells(List<List<Cell>> gameField, int x, int y, ref int flagsOnField)
        {
            int numOfEmpty = 0;

            if (x < 0 || y < 0 || x > gameField.Count - 1 || y > gameField.Count - 1)
            {
                return 0;
            }

            List<Cell> row = gameField[x];
            if (!row[y].IsHidden)
            {
                return 0;
            }
            else if (row[y].OpenContent == ContentInCell.Empty)
            {
                row[y].IsHidden = false;
                numOfEmpty++;

                if (row[y].IsFlaged)
                {
                    row[y].IsFlaged = false;
                    flagsOnField--;
                }

                numOfEmpty += OpenCells(gameField, x + 1, y, ref flagsOnField);

                numOfEmpty += OpenCells(gameField, x, y + 1, ref flagsOnField);

                numOfEmpty += OpenCells(gameField, x - 1, y, ref flagsOnField);

                numOfEmpty += OpenCells(gameField, x, y - 1, ref flagsOnField);
            }
            else if (row[y].OpenContent != ContentInCell.Empty)
            {
                row[y].IsHidden = false;

                if (!row[y].IsMined)
                {
                    numOfEmpty++;
                }

                if (row[y].IsFlaged)
                {
                    row[y].IsFlaged = false;
                    flagsOnField--;
                }

                return numOfEmpty;
            }

            return numOfEmpty;
        }
    }
}
