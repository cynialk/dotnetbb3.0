using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetbb3._0
{
    class Tile
    {
        public List<Tile> Neighbours = new List<Tile>();
        public int[] Position { get; set; }
        public Player StoredPlayer;

        public Tile(int[] pos)
        {
            Position = pos;
        }

        public void FindNeighbours()
        {
            Neighbours = new List<Tile>();
            //Add neighbours
            for (int x = Position[0] - 1; x <= Position[0] + 1; ++x)
            {
                for (int y = Position[1] - 1; y <= Position[1] + 1; ++y)
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
        public int GetAssists(string team)
        {
            int assists = 0;
            foreach (Tile NeighbouringTile in Neighbours)
            {
                if (NeighbouringTile.StoredPlayer == null)
                {
                    continue;
                }
                else if (NeighbouringTile.StoredPlayer.Team != StoredPlayer.Team)
                {
                    if (NeighbouringTile.GetTackleZones(team) < 2) assists--;
                }
                else
                {
                    if ( team == "Home")
                    {
                        if (NeighbouringTile.GetTackleZones("Home") < 2) assists++;
                    }
                    else
                    {
                        if (NeighbouringTile.GetTackleZones("Away") < 2) assists++;
                    }
                    
                }
            }
            return assists;
        }
        public int GetTackleZones(string team)
        {
            int amount = 0;
            foreach (Tile NeighbouringTile in Neighbours)
            {
                if (NeighbouringTile == null || NeighbouringTile.StoredPlayer == null) continue;
                if (NeighbouringTile.StoredPlayer.Team != team)
                {
                    amount++;
                }
            }
            return amount;
        }
    } 
}
