using System.Collections.Generic;

using Minesweeper.Enums;

namespace Minesweeper.Models.Helpers
{
    public class CellsOpener
    {
        public void OpenCells(List<List<Cell>> gameField, int x, int y, ref int flagsOnField, out int numOfEmpty)
        {
            numOfEmpty = 0;

            if (x < 0 || y < 0 || x > gameField.Count - 1 || y > gameField.Count - 1)
            {
                return;
            }

            List<Cell> row = gameField[x];
            if (!row[y].IsHidden)
            {
                return;
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

                int numOfEmptyTemp;

                OpenCells(gameField, x + 1, y, ref flagsOnField, out numOfEmptyTemp);
                numOfEmpty += numOfEmptyTemp;

                OpenCells(gameField, x, y + 1, ref flagsOnField, out numOfEmptyTemp);
                numOfEmpty += numOfEmptyTemp;

                OpenCells(gameField, x - 1, y, ref flagsOnField, out numOfEmptyTemp);
                numOfEmpty += numOfEmptyTemp;

                OpenCells(gameField, x, y - 1, ref flagsOnField, out numOfEmptyTemp);
                numOfEmpty += numOfEmptyTemp;
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

                return;
            }
        }
    }
}
