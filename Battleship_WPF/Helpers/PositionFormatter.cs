using BattleshipLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_WPF
{
    public class PositionFormatter
    {
        public string GetPosition(Position coords)
        {
            string letter = string.Empty;

            switch ((Letters)coords.OX)
            {
                case Letters.A:
                    letter = Letters.A.ToString();
                    break;
                case Letters.B:
                    letter = Letters.B.ToString();
                    break;
                case Letters.C:
                    letter = Letters.C.ToString();
                    break;
                case Letters.D:
                    letter = Letters.D.ToString();
                    break;
                case Letters.E:
                    letter = Letters.E.ToString();
                    break;
                case Letters.F:
                    letter = Letters.F.ToString();
                    break;
                case Letters.G:
                    letter = Letters.G.ToString();
                    break;
                case Letters.H:
                    letter = Letters.H.ToString();
                    break;
                case Letters.I:
                    letter = Letters.I.ToString();
                    break;
                case Letters.J:
                    letter = Letters.J.ToString();
                    break;
                default:
                    break;
            }

            return string.Format("{0}{1}", coords.OY + 1, letter).ToUpper();
        }
    }
}
