using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetbb3._0
{
    static class InputHandler
    {
        public static Random random = new Random();
        public static int[] Move8(ConsoleKey input)
        {
            int[] moveto = { 0, 0 };
            switch(input)
            {
                case ConsoleKey.NumPad7:
                    moveto[0] = -1;
                    moveto[1] = -1;
                    break;
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                case ConsoleKey.NumPad8:
                    moveto[1] = -1;
                    break;
                case ConsoleKey.NumPad9:
                    moveto[0] = 1;
                    moveto[1] = -1;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.NumPad4:
                    moveto[0] = -1;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                case ConsoleKey.NumPad6:
                    moveto[0] = 1;
                    break;
                case ConsoleKey.NumPad1:
                    moveto[0] = -1;
                    moveto[1] = 1;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                case ConsoleKey.NumPad2:
                    moveto[1]=1;
                    break;
                case ConsoleKey.NumPad3:
                    moveto[0] = 1;
                    moveto[1] = 1;
                    break;
            }
            return moveto;
        }

        public static string ChoiceMenu(List<string> choices)
        {
            int CursorPosition = 0;
            while (true)
            {
                RenderHandler.RenderPitch();

                for (int i = 0; i < choices.Count; i++)
                {
                    if (CursorPosition == i)
                    {
                        Console.WriteLine(">{0}<", choices[i]);
                    }
                    else
                    {
                        Console.WriteLine(" {0}", choices[i]);
                    }
                }
                if (CursorPosition == choices.Count)
                {
                    Console.WriteLine(">{0}<", "Exit");
                }
                else
                {
                    Console.WriteLine(" {0}", "Exit");
                }

                ConsoleKey input = Console.ReadKey().Key;
                CursorPosition = (CursorPosition + InputHandler.Move2(input)) % (choices.Count + 1);
                if (CursorPosition < 0) CursorPosition = choices.Count;
                if (input == ConsoleKey.Enter)
                {
                    if (CursorPosition == choices.Count)
                    {
                        return "Exit";
                    }
                    return choices[CursorPosition];
                }
            }
        }

        public static int Roll1d6()
        {
            int rolled = random.Next(1, 6);
            Console.WriteLine("Rolled: " + rolled);
            Console.ReadKey();
            return rolled;
        }

        public static int Move2(ConsoleKey input)
        {
            int moveto = 0;
            switch (input)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                case ConsoleKey.NumPad8:
                    moveto = -1;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                case ConsoleKey.NumPad2:
                    moveto = 1;
                    break;
            }
            return moveto;
        }
    }
}
