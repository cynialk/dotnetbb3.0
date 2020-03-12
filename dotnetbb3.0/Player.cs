using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetbb3._0
{
    public class Player
    {
        public string DisplayName { get; set; }
        public int[] Stats { get; set; }
        public string PlayerName { get; set; }
        public string[] Skills { get; set; }
        public string Team { get; set; }
        public int[] Position { get; set; }
        public bool Stunned { get; set; }
        public bool Proned { get; set; }
        public bool Used { get; set; }

        public Player(string InputName, string InputDisplayName, int[] InputStats, string[] InputSkills, string InputTeam)
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
        public void Move()
        {
            for (int Steps = 0; Steps <= Stats[0]+2; Steps++)
            {
                RenderHandler.RenderPitch();
                Cursor.Position = new int[] { Position[0], Position[1] };
                Console.WriteLine("Movement left: " + (Stats[0]-Steps));
                ConsoleKey input = Console.ReadKey().Key;
                int[] movement = InputHandler.Move8(input);
            }
        }

        public void Stun()
        {
            Stunned = true;
            Used = true;
        }
    }
}
