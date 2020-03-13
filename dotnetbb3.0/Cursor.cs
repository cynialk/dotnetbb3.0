using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetbb3._0
{
    static class Cursor
    {
        public static int[] Position = { 5, 10 };
        public static Player HoldingPlayer;
        public static bool LastTileHadPlayer = false;

        public static string MoveCursorOnPitch(string onTeam=null)
        {
            while (true)
            {
                if (!LastTileHadPlayer && HoldingPlayer != null)
                {
                    PitchHandler.Pitch[Position[0], Position[1]].StoredPlayer = null;
                }
                ConsoleKey input = Console.ReadKey().Key;
                int[] movement = InputHandler.Move8(input);
                if (input == ConsoleKey.Enter)
                {
                    if (HoldingPlayer != null && PitchHandler.Pitch[Position[0],Position[1]].StoredPlayer == null)
                    {
                        PitchHandler.Pitch[Position[0], Position[1]].StoredPlayer = HoldingPlayer;
                        HoldingPlayer.Position = new int[] { Position[0],Position[1]};
                        HoldingPlayer = null;
                        continue;
                    }
                    return InputHandler.ChoiceMenu(TurnHandler.AvailableActions);

                }
                if (onTeam == null)
                {
                    Position[0] = (Position[0] + movement[0]) % PitchHandler.Pitch.GetLength(0);
                    if (Position[0] < 0) Position[0] = (PitchHandler.Pitch.GetLength(0) - 1);
                }
                else if (onTeam == "Home")
                {
                    Position[0] = (Position[0] + movement[0]) % 13;
                    if (Position[0] < 0) Position[0] = 12;
                }
                else if (onTeam == "Away")
                {
                    Position[0] = ((Position[0] + movement[0]-13) % 13)+13;
                    if (Position[0] < 13) Position[0] = (PitchHandler.Pitch.GetLength(0) - 1);
                }
                Position[1] = (Position[1] + movement[1]) % PitchHandler.Pitch.GetLength(1);
                if (Position[1] < 0) Position[1] = (PitchHandler.Pitch.GetLength(1) - 1);


                if (PitchHandler.Pitch[Position[0], Position[1]].StoredPlayer != null)
                {
                    LastTileHadPlayer = true;
                    RenderHandler.RenderPitch();
                    PitchHandler.Pitch[Position[0], Position[1]].StoredPlayer.WritePlayerData();
                }
                else if (HoldingPlayer != null)
                {
                    LastTileHadPlayer = false;
                    PitchHandler.Pitch[Position[0], Position[1]].StoredPlayer = HoldingPlayer;
                    RenderHandler.RenderPitch();
                }
                else
                {
                    RenderHandler.RenderPitch();
                }
            }
        }
    }
}
