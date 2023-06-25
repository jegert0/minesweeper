using System;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

//-Classes-, -arrays-, -input/output-, -loops-, -if statements-
//-You will need to use a class for the player with name and high scores-
//-Use methods to do the processing of the moves-




namespace minesweeper 
{
    internal class Program
    {
        public static bool GameState = true;
        public static int keepScore = 0;
        public static int totalBomb = 0;
        
        

        //method for displaying different maps
        public static void display(string[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j]);
                }
            }
            Console.WriteLine();
        }
        //method for generating random bomb placments on the map
        public static void bombGen(string[,] arr)
        {
            Random rnd = new Random();
            totalBomb = rnd.Next(4, 7);

            for (int i = 0; i < totalBomb; i++)
            {
                int col = rnd.Next(1, 7);
                int row = rnd.Next(1, 7);

                arr[row, col] = " * ";
            }



        }
       
        //method for counting surroding spaces for bombs
        public static void countBomb(string[,] arr)
        {
            for (int i = 1; i < 7; i++)
            {
                
                for (int j = 1; j < 7; j++)
                {   int count = 0;
                    if (arr[i, j] == " * ") 
                    {
                        continue;
                    }
                    for (int y = 0; y < 3; y++)
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            //Console.WriteLine("{0},{1},{2},{3},{4}", x, y, i, j, count);
                            if (arr[i-1+y,j-1+x] == " * ")
                            {
                                count++;
                                
                            }
                        }
                    }
                    arr[i, j] = " " + Convert.ToString(count) + " ";
                }
            }
            
        }

        //generates answer key map
        public static void answerKey(string[,] arr)
        {
            bombGen(arr);
            countBomb(arr);
        }

        //method for taking input for play
        public static void play(string input, string[,] mapView, string[,] arr)
        {
            char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f' };
            int[] index = {1, 2, 3, 4 ,5 ,6 ,7 };
            int row = 0;
            int col = 0;

            if (letters.Contains(input[1])){
                row = index[Array.IndexOf(letters, input[1])];
            }
            if (letters.Contains(input[2]))
            {
                col = index[Array.IndexOf(letters, input[2])];
            }
            if (input[0] == 'm')
            {
                if (mapView[row,col] == " * ")
                {
                    keepScore++;
                    totalBomb--;
                    if (totalBomb == 0)
                    {
                        Console.WriteLine("YOU WIN");
                        display(mapView);
                        GameState = false;
                    }
                }
                arr[row, col] = " X ";

            }
            else if (input[0] == 'o')
            {
                if (mapView[row, col] == " * ")
                {
                    Console.WriteLine("GAME OVER");
                    display(mapView);
                    GameState = false;
                }
                else
                {
                    arr[row, col] = mapView[row, col];
                }
            }
            else
            {
                Console.WriteLine("Unknown Command");
            }
        }

        //player class with name and score; score caluclated by correct amount of bombs deduced
        class Player
        {
            public int score;
            public string name = " ";

            public Player(string nm, int sc)
            {
                score = sc; name = nm;
            }
        }
        static void Main(string[] args)
        {
            
            string[,] mapNew =
        {
            {"   ", " a "," b ", " c ", " d ", " e ", " f ", "  " },
            {" a ", " # "," # ", " # ", " # ", " # ", " # ", "  " },
            {" b ", " # "," # ", " # ", " # ", " # ", " # ", "  " },
            {" c ", " # "," # ", " # ", " # ", " # ", " # ", "  " },
            {" d ", " # "," # ", " # ", " # ", " # ", " # ", "  " },
            {" e ", " # "," # ", " # ", " # ", " # ", " # ", "  " },
            {" f ", " # "," # ", " # ", " # ", " # ", " # ", "  " },
            {"   ", "   ","   ", "   ", "   ", "   ", "   ", "  " },
        };





            string[,] map = mapNew.Clone() as string[,];
            string[,] mapView = mapNew.Clone() as string[,];

            answerKey(mapView);

            //display(mapView); //view the answer key
            Console.WriteLine("Score is calculated by how many bombs you can guess");

            do
            {
                Console.WriteLine("Total Bombs Left:{0}", totalBomb);
                display(map);

                Console.WriteLine("Input m (mark) or o (open) followed by row and col >>> \n");
                string input = Console.ReadLine();
                if (input == "quit")
                {
                    break;
                }
                play(input, mapView, map);


            } while (GameState);

            Console.Write("Please Enter Username >>> ");
            string username = Console.ReadLine();
            Player user = new Player(username, keepScore);
            Console.WriteLine("Username: " + user.name + "\nScore: " + user.score);

            


        }
    }
}