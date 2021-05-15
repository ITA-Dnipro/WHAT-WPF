using System;
using System.Collections.Generic;
using Tetris.Model.Shape;

namespace Tetris.Model
{
    class ShapeCreator 
    {
        readonly Random _rnd = new Random();
        Dictionary<int, BaseShape> shapes;

        private void InitializeShapeDictionary()
        {
            shapes.Add(shapes.Count, new ShapeI());
            shapes.Add(shapes.Count, new ShapeJ());
            shapes.Add(shapes.Count, new ShapeL());
            shapes.Add(shapes.Count, new ShapeO());
            shapes.Add(shapes.Count, new ShapeS());
            shapes.Add(shapes.Count, new ShapeT());
            shapes.Add(shapes.Count, new ShapeZ());
        }

        public BaseShape CreateNewShape(int ColumnSize)
        {
            shapes = new Dictionary<int, BaseShape>();
            InitializeShapeDictionary();

            BaseShape shape = GetRandomShape();
            shape.Create(0, ColumnSize / 2);

            return shape;
        }

        private BaseShape GetRandomShape()
        {
            return shapes[_rnd.Next(0, shapes.Count)];
        }
    }
}
