using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetbb3._0
{
    static class PitchHandler
    {
        public static Tile[,] Pitch = new Tile[26, 15];

        public static void InitPitch()
        {
            for (int x = 0; x < Pitch.GetLength(0); x++)
            {
                for (int y = 0; y < Pitch.GetLength(1); y++)
                {
                    int[] pos = { x, y };
                    Pitch[x, y] = new Tile(pos);
                }
            }
        }
    }
}
