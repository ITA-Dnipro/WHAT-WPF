using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Model.Shape;

namespace Tetris.Model
{
    class ShapeCreator 
    {
        public BaseShape CreateNewShape()
        {
            BaseShape shape = GetRandomShape();
            shape.Create(0, GameManager.COLUMNS / 2);

            return shape;
        }

        private BaseShape GetRandomShape()
        {
            BaseShape shape = new ShapeO();
            Random _rnd = new Random();

            switch (_rnd.Next(0, 7))
            {
                case 0:
                    {
                        shape = new ShapeO();
                        break;
                    }
                case 1:
                    {
                        shape = new ShapeI();
                        break;
                    }
                case 2:
                    {
                        shape = new ShapeL();
                        break;
                    }
                case 3:
                    {
                        shape = new ShapeZ();
                        break;
                    }
                case 4:
                    {
                        shape = new ShapeT();
                        break;
                    }
                case 5:
                    {
                        shape = new ShapeJ();
                        break;
                    }
                case 6:
                    {
                        shape = new ShapeS();
                        break;
                    }
            }

            return shape;
        }
    }
}
