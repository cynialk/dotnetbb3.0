using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetbb3._0
{
    class Tile
    {
        public List<Tile> Neighbours = new List<Tile>();
        public int[] position { get; set; }
        public Player StoredPlayer;

        public Tile(int[] pos)
        {
            position = pos;

            //Add neighbours
            for (int x = this.position[0] - 1; x <= this.position[0] + 1; x++)
            {
                for (int y = this.position[1] - 1; y <= this.position[1] + 1; y++)
                {
                    if (x >= 0 && x < PitchHandler.Pitch.GetLength(0) && y >= 0 && y < PitchHandler.Pitch.GetLength(1))
                    {
                        if (!this.Equals(PitchHandler.Pitch[x, y]))
                        {
                            Neighbours.Add(PitchHandler.Pitch[x, y]);
                        }
                    }
                }
            }
        }
    } 
}
