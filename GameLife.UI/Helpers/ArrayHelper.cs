using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.UI.Helpers
{
    public static class ArrayHelper
    {
        public static void Each<T>(this T[,] array, Func<int, int,T> action)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = action.Invoke(i, j);
                }
            }
        }

    }
}
