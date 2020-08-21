using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameLife.UI
{
    [Serializable]
    public class Map
    {
        public readonly int Columns;
        public readonly int Rows;
        public bool haveBorder = false;

        public int generation;

        public byte[,] _current;
        private byte[,] _next;

        public Map(int Width, int Height, bool haveBorder = false)
        {
            this.Columns = Width;
            this.Rows = Height;
            this._current = new byte[Height, Width];
            this._next = new byte[Height, Width];
            this.haveBorder = haveBorder;
        }

        public byte this[int i, int j]
        {
            get
            {
                return _current[i, j];
            }
            set
            {
                _next[i, j] = value;
            }
        }

        public void Swap()
        {
            var temp = _current;
            _current = _next;
            _next = temp;
        }

        public int GetNeighborsCount(int y, int x)
        {
            int neighborsCount = 0;
            if (haveBorder)
            {
                //поле имеет границы
                int row_limit = _current.GetLength(0)-1;
                int column_limit = _current.GetLength(1)-1;
                if (row_limit > 0 && column_limit> 0)
                {
                    for (int i = Math.Max(0, y - 1); i <= Math.Min(y + 1, row_limit); i++)
                    {
                        for (int j = Math.Max(0, x - 1); j <= Math.Min(x + 1, column_limit); j++)
                        {
                            if ((i != y || j != x) && _current[i, j] != 0)
                            {
                                neighborsCount++;
                            }
                        }
                    }
                }
            }
            else
            {
                //поле замкнуто
                var Yinc = (y + 1) % Rows;//инкрементированное значение координаты Y
                var Ydec = (Rows + y - 1) % Rows;//декрементированное значение координаты Y
                var Xinc = (x + 1) % Columns;//инкрементированное значение координаты X
                var Xdec = (Columns + x - 1) % Columns;//декрементированное значение координаты X
                if (_current[Yinc, Xdec] != 0) neighborsCount++;// проворяем клетку выше и левее
                if (_current[Yinc, (x)] != 0) neighborsCount++;// проворяем клетку выше
                if (_current[Yinc, Xinc] != 0) neighborsCount++;// проворяем клетку выше и правее
                if (_current[(y), Xdec] != 0) neighborsCount++;// проворяем клетку левее
                if (_current[(y), Xinc] != 0) neighborsCount++;// проворяем клетку правее
                if (_current[Ydec, Xdec] != 0) neighborsCount++;// проворяем клетку ниже и левее
                if (_current[Ydec, (x)] != 0) neighborsCount++;// проворяем клетку ниже
                if (_current[Ydec, Xinc] != 0) neighborsCount++;// проворяем клетку ниже и правее
            }
            return neighborsCount;
        }

    }
}
