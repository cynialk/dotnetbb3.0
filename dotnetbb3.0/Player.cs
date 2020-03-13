using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetbb3._0
{
    class Player
    {
        public string DisplayName { get; set; }
        public int[] Stats { get; set; }
        public string PlayerName { get; set; }
        public List<string> Skills { get; set; }
        public string Team { get; set; }
        public int[] Position { get; set; }
        public bool Stunned { get; set; }
        public bool Proned { get; set; }
        public bool Used { get; set; }
        bool inTackleZone;

        public Player(string InputName, string InputDisplayName, int[] InputStats, List<string> InputSkills, string InputTeam)
        {
            PlayerName = InputName;
            DisplayName = InputDisplayName;
            Stats = InputStats;
            Skills = InputSkills;
            Team = InputTeam;
            
        }
        public void WritePlayerData() { 
        
            Console.WriteLine("Selected Player: " + PlayerName);
            Console.WriteLine("MA: {0}, ST: {1}, AG: {2}, AV: {3}", Stats[0], Stats[1], Stats[2], Stats[3]);
            Console.WriteLine("Skills: " + String.Join(", ", Skills));
            Console.WriteLine("Coordinates: x: {0}, y: {1}",Position[0],Position[1]);
        }
        public void Move() //Unfinished
        {
            for (int Steps = 0; Steps < Stats[0]+2; Steps++)
            {
                if (PitchHandler.Pitch[Position[0],Position[1]].GetTackleZones(Team) > 0)
                {
                    Console.WriteLine("Need to dodge");
                    inTackleZone = true;
                }
                else
                {
                    inTackleZone = false;
                }
                Console.WriteLine("Movement left: " + (Stats[0]-Steps));
                Console.WriteLine("Press Enter to end action");
                ConsoleKey input = Console.ReadKey().Key;
                if (input == ConsoleKey.Enter)
                {
                    Used = true;
                    return;
                }
                int[] movement = InputHandler.Move8(input);
                int[] newPos = new int[] { Position[0] + movement[0], Position[1] + movement[1] };
                if ( newPos[0] >= 0 && newPos[1] >= 0 && newPos[0] < PitchHandler.Pitch.GetLength(0) && newPos[1] < PitchHandler.Pitch.GetLength(1) && PitchHandler.Pitch[newPos[0],newPos[1]].StoredPlayer == null)
                {
                    MovePlayerToTileAtPosition(new int[] { newPos[0], newPos[1] });
                    Cursor.Position = new int[] { Position[0], Position[1] };
                    if (inTackleZone && !DodgeRoll(PitchHandler.Pitch[Position[0], Position[1]].GetTackleZones(Team)))
                    {
                        Proned = true;
                        TurnHandler.TurnOver();
                        return;
                    }
                    RenderHandler.RenderPitch();
                }
                else
                {
                    Steps--;
                    continue;
                }
            }
            Used = true;
        }
        public bool DodgeRoll(int TackleZones)
        {
            bool success = AgRoll(1 - TackleZones);
            Console.WriteLine("Success: {0}, Tacklezones: {1}, Agility: {2}, Needed: {3}", success, TackleZones, Stats[2], 7 - Stats[2] - (1 - TackleZones));
            Console.ReadKey();
            return success;
        }
        public bool AgRoll(int modifiers)
        {
            int roll = InputHandler.Roll1d6();
            if (roll == 6) return true;
            else if (roll == 1) return false;
            else if ((roll + modifiers) > 6 - Stats[2]) return true;
            else return false;
        }
        public void MovePlayerToTileAtPosition(int[] NewPosition)
        {
            PitchHandler.Pitch[Position[0], Position[1]].StoredPlayer = null;
            PitchHandler.Pitch[NewPosition[0], NewPosition[1]].StoredPlayer = this;
            Position = new int[] { NewPosition[0], NewPosition[1] };
        }
        public void Stun()
        {
            Stunned = true;
            Used = true;
        }
        public void KO()
        {
            Stunned = false;
            Used = false;
            if (Team == "Home")
            {
                TurnHandler.HomeTeam.Remove(this);
                TurnHandler.HomeKO.Add(this);
            }
            else
            {
                TurnHandler.AwayTeam.Remove(this);
                TurnHandler.AwayKO.Add(this);
            }
        }
        public void Injury()
        {
            if (Team == "Home")
            {
                TurnHandler.HomeTeam.Remove(this);
                TurnHandler.HomeInjured.Add(this);
            }
            else
            {
                TurnHandler.AwayTeam.Remove(this);
                TurnHandler.AwayInjured.Add(this);
            }
        }
        public void ArmorRoll()
        {
            int Rolled = InputHandler.Roll1d6() + InputHandler.Roll1d6();
            if (Rolled < 8)
            {
                Stun();
            }
            else if (Rolled < 10)
            {
                KO();
            }
            else if (Rolled < 13)
            {
                Injury();
            }
        }
    }
}
