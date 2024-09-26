using System;

namespace Balls.Source.Logic.GameBoard
{
    public sealed class GridSize
    {
        public GridSize(int width, int height)
        {
            if (width <= 0)
                throw new InvalidOperationException($"{nameof(width)} must be greater than zero.");
            
            if (height <= 0)
                throw new InvalidOperationException($"{nameof(height)} must be greater than zero.");
            
            Width = width;
            Height = height;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
    }
}