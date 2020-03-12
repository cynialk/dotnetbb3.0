using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetbb3._0
{
    static class RenderHandler
    {
        public static void RenderPitch()
        {
            Console.Clear();
            Tile[,] Pitch = PitchHandler.Pitch;
            for (int y = 0; y < Pitch.GetLength(1); y++)
            {
                for(int x = 0; x < Pitch.GetLength(0); x++)
                {
                    if (Cursor.Position[0] == x && Cursor.Position[1] == y)
                    {
                        Console.Write(">");
                    }
                    else
                    {
                        Console.Write("[");
                    }
                    if (Pitch[x,y].StoredPlayer != null)
                    {
                        if (Pitch[x, y].StoredPlayer.Team == "Home")
                        {
                            if (!Pitch[x, y].StoredPlayer.Proned)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                            }
                            
                        }
                        else
                        {
                            if (!Pitch[x, y].StoredPlayer.Proned)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                            }

                        }

                        if (!Pitch[x, y].StoredPlayer.Stunned)
                        {
                            Console.Write(Pitch[x, y].StoredPlayer.DisplayName);
                        }
                        else
                        {
                            Console.Write("*");
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else if (x == 13 || x == 12)
                    {
                        Console.Write("|");
                    }
                    else if (y == 3 || y == 11)
                    {
                        Console.Write("-");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    if (Cursor.Position[0] == x && Cursor.Position[1] == y)
                    {
                        Console.Write("<");
                    }
                    else
                    {
                        Console.Write("]");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
