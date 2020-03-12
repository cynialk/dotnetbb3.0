using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dotnetbb3._0
{
    static class TurnHandler
    {
        public static int HomeTurns;
        public static int AwayTurns;
        public static List<string> AvailableActions;
        public static string ActiveTeam;

        public static List<Player> HomeTeam = new List<Player> {
            new Player("Bob Skifford1", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Home"),
            new Player("Bob Skifford2", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Home"),
            new Player("Bob Skifford3", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Home"),
            new Player("Bob Skifford4", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Home"),
            new Player("Bob Skifford5", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Home"),
            new Player("Bob Skifford6", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Home"),
            new Player("Bob Skifford7", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Home"),
            new Player("Bob Skifford8", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Home"),
            new Player("Bob Skifford9", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Home"),
            new Player("Bob Skifford10", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Home"),
            new Player("Bob Skifford11", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Home"),
        };
        public static List<Player> AwayTeam = new List<Player> {
            new Player("McMurty1", "L", new int[] { 996, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Away"),
            new Player("McMurty2", "L", new int[] { 996, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Away"),
            new Player("McMurty3", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Away"),
            new Player("McMurty4", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Away"),
            new Player("McMurty5", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Away"),
            new Player("McMurty6", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Away"),
            new Player("McMurty7", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Away"),
            new Player("McMurty8", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Away"),
            new Player("McMurty9", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Away"),
            new Player("McMurty10", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Away"),
            new Player("McMurty11", "L", new int[] { 6, 3, 3, 8 }, new List<string> {"Block", "Pass", "Sure Hands" }, "Away"),
        };

        public static List<Player> HomeReserves = new List<Player>();
        public static List<Player> AwayReserves = new List<Player>();
        public static List<Player> HomeOnField = new List<Player>();
        public static List<Player> AwayOnField = new List<Player>();


        public static  void initMatch()
        {
            PitchHandler.InitPitch();
            //TODO: Weather
            //TODO: Fame
            //TODO: Coin toss
            HomeTurns = 16;
            AwayTurns = 16;
            Player[] temp = new Player[HomeTeam.Count];
            HomeTeam.CopyTo(temp);
            HomeReserves = temp.ToList();
            AwayTeam.CopyTo(temp);
            AwayReserves = temp.ToList();
            SetupTeam("Away");
            SetupTeam("Home");
            StartTurn("Away");

        }
        public static void TurnOver()
        {
            EndTurn(ActiveTeam);
        }
        public static void StartTurn(string team) //Unfinished
        {
            ActiveTeam = team;
            AvailableActions = new List<string> { "Move","Block","Blitz","Pass","Handover"};
            while (true)
            {
                RenderHandler.RenderPitch();
                string SelectedChoice = Cursor.MoveCursorOnPitch();
                Player SelectedPlayer = PitchHandler.Pitch[Cursor.Position[0], Cursor.Position[1]].StoredPlayer;
                if (SelectedPlayer == null && SelectedPlayer.Team != team && SelectedPlayer.Used && SelectedPlayer.Stunned) continue;
                switch (SelectedChoice)
                {
                    case "Move":
                        SelectedPlayer.Move();
                        break;
                }
            }
        }

        public static void EndTurn(string team, List<Player> Exceptions=null)
        {
            List<Player> ActiveOnField;
            Exceptions = Exceptions ?? new List<Player>();
            if (team == "Home")
            {
                ActiveOnField = HomeOnField;
            }
            else
            {
                ActiveOnField = AwayOnField;
            }
            foreach (Player player in ActiveOnField)
            {
                if (Exceptions.Contains(player))
                {
                    continue;
                }
                else if (player.Stunned)
                {
                    player.Stunned = false;
                    player.Proned = true;
                    player.Used = false;
                }
                else if (player.Used)
                {
                    player.Used = false;
                }
            }
        }

        public static void SetupTeam(string team)
        {
            List<Player> ActiveReserves;
            List<Player> ActiveOnField;
            if (team == "Home")
            {
                ActiveOnField = HomeOnField;
                ActiveReserves = HomeReserves;
                Cursor.Position = new int[] { 0, 0 };
            }
            else
            {
                ActiveOnField = AwayOnField;
                ActiveReserves = AwayReserves;
                Cursor.Position = new int[] { 13, 0 };
            }
            AvailableActions = new List<string> { "Return the Player on this tile to the reserves", "Open the reserves", "Move Player", "End Setup"};
            while (true)
            {
                RenderHandler.RenderPitch();
                string SelectedChoice = Cursor.MoveCursorOnPitch(team);
                switch (SelectedChoice)
                {
                    case "Open the reserves":
                        Player SelectedPlayer = GetPlayerFromList(ActiveReserves);
                        ActiveReserves.Remove(SelectedPlayer);
                        ActiveOnField.Add(SelectedPlayer);
                        Cursor.HoldingPlayer = SelectedPlayer;
                        break;
                    case "Return the Player on this tile to the reserves":
                        if (PitchHandler.Pitch[Cursor.Position[0],Cursor.Position[1]].StoredPlayer != null)
                        {
                            ActiveOnField.Remove(PitchHandler.Pitch[Cursor.Position[0], Cursor.Position[1]].StoredPlayer);
                            ActiveReserves.Add(PitchHandler.Pitch[Cursor.Position[0], Cursor.Position[1]].StoredPlayer);
                            PitchHandler.Pitch[Cursor.Position[0], Cursor.Position[1]].StoredPlayer = null;
                        }
                        break;
                    case "Move Player":
                        Cursor.HoldingPlayer = PitchHandler.Pitch[Cursor.Position[0], Cursor.Position[1]].StoredPlayer;
                        PitchHandler.Pitch[Cursor.Position[0], Cursor.Position[1]].StoredPlayer = null;
                        break;
                    case "End Setup":
                        int onLoS = 0;
                        int inWideTop = 0;
                        int inWideBot = 0;
                        foreach (Player player in ActiveOnField)
                        {
                            if ((player.Position[0] == 12 || player.Position[0] == 13) && (player.Position[1] > 3 || player.Position[1] < 11)) onLoS++;
                            if (player.Position[1] <= 3) inWideTop++;
                            if (player.Position[1] >= 11) inWideBot++;
                        }
                        if (inWideBot > 2 || inWideTop > 2)
                        {
                            Console.WriteLine("You may only have two players in the widezones");
                            Console.ReadKey();
                            break;
                        }
                        else if (onLoS < 0) //Fix count, debug rn
                        {
                            Console.WriteLine("You must have at least 3 players on the line of scrimage");
                            Console.WriteLine("(You currently have: {0} players on the line of scrimage)", onLoS);
                            Console.ReadKey();
                            break;
                        }
                        else if (ActiveOnField.Count > 11)
                        {
                            Console.WriteLine("You cannot have more than 11 players on the field");
                            Console.ReadKey();
                            break;
                        }
                        else if (ActiveOnField.Count < 0) //Fix playercount, debug rn
                        {
                            Console.WriteLine("You must place as many players as you can on the field (up to 11 players)");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            return;
                        }
                }
            }
        }

        public static Player GetPlayerFromList(List<Player> PlayerList)
        {
            List<string> Players = new List<string>();
            foreach (Player player in PlayerList)
            {
                Players.Add(player.PlayerName);
            }
            string selectedPlayerName = InputHandler.ChoiceMenu(Players);
            foreach (Player player in PlayerList)
            {
                if (player.PlayerName == selectedPlayerName) return player;
            }
            return null;
        }
    }
}
