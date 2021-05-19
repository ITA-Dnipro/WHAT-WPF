using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestButtons
{
    public class Game 
    {
        public MarkType?[,] markTypes { get; set; }
        public int Quantity { get; set; }

        public Game(int quantity)
        {
            Quantity = quantity;
            markTypes = new MarkType?[Quantity, Quantity];
        }

        public (bool, string) CheckResults()
        {
            bool isWin1 = false;
            string winner = "Nobody win";
            for (int i = 0; i < Quantity; i++)
            {
                if (CheckOnColum(i))
                {
                    isWin1 = true;
                    winner = CheckWhoIsWinner(i);
                    break;
                }
                if (CheckOnRow(i))
                {
                    isWin1 = true;
                    winner = CheckWhoIsWinner(i);
                    break;
                }
            }
            if (CheckOnMainDiag())
            {
                isWin1 = true;
                winner = CheckWhoIsWinner(0);
            }
            if (CheckOnDoubleDiag())
            {
                isWin1 = true;
                winner = markTypes[Quantity - 1, 0].ToString();
            }

            return (isWin1, winner);
        }

        private string CheckWhoIsWinner(int i)
        {
            return markTypes[i, i].ToString();
        }

        public void ClearMarks()
        {
            for (int i = 0; i < Quantity; i++)
            {
                for (int j = 0; j < Quantity; j++)
                {
                    markTypes[i, j] = null;
                }
            }
        }

        private bool CheckOnColum(int j)
        {
            int countEquas = 0;

            if (markTypes[j, j] != null)
            {
                for (int k = 0; k < Quantity; k++)
                {
                    if (markTypes[k, j] != null && markTypes[k, j] == markTypes[j, j])
                    {
                        countEquas++;
                    }
                }
            }

            if (countEquas == Quantity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckOnRow(int j)
        {
            int countEquas = 0;

            if (markTypes[j, j] != null)
            {
                for (int k = 0; k < Quantity; k++)
                {
                    if (markTypes[j, k] != null && markTypes[j, k] == markTypes[j, j])
                    {
                        countEquas++;
                    }
                }
            }

            if (countEquas == Quantity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckOnMainDiag()
        {
            int countEquals = 0;
            for (int j = 0; j < Quantity; j++)
            {
                if (markTypes[j, j] != null && markTypes[0, 0] == markTypes[j, j])
                {
                    countEquals++;
                }
            }
            if (countEquals == Quantity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckOnDoubleDiag()
        {
            int integer = Quantity - 1;
            int countEquals = 0;
            if (markTypes[0, Quantity - 1] != null)
            {

                for (int j = 0; j < Quantity; j++)
                {
                    if (markTypes[integer, j] != null && markTypes[0, Quantity - 1] == markTypes[integer, j])
                    {
                        countEquals++;
                    }
                    integer--;
                }
                integer = Quantity - 1;
                if (countEquals == Quantity)
                {
                    return true;
                }
            }
            return false;
        }


        public MarkType? this[int row, int column]
        {
            get => markTypes[row, column];
            set
            {
                markTypes[row, column] = value;
            }
        }
    }
}
