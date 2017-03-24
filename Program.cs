using System;
using System.Collections.Generic;
using System.IO;

namespace astar { 
class Solution
{
    

    static void astar(int r, int c, int pacman_r, int pacman_c, int food_r, int food_c, String[] grid)
    {
   

            SimplePriorityQue openList = new SimplePriorityQue();
            //List<Spot> openList = new List<Spot>();
            HashSet<Spot> closedList = new HashSet<Spot>();

            Spot[,] Grid = MakeReasonableGrid(r, c, pacman_r, pacman_c, food_r, food_c, grid);

            var start = Grid[pacman_r, pacman_c];
            var end = Grid[food_r, food_c];

            openList.insert(start);

            Spot current = null;
            while (!openList.isEmpty())
            {
                current = openList.getElement();
                if (!current.accessible) continue;

                if (current == end)
                {
                    break;
                }
                closedList.Add(current);

                foreach (var item in current.neighbors)
                {
                    if (closedList.Contains(item)) continue;
                    if (!item.accessible) continue;
                    double tempG = current.g + 1;
                    double heuristic = Math.Abs(end.I - current.I) + Math.Abs(end.J - current.J);
                    if (!openList.contains(item))
                    {
                        item.h = heuristic;
                        item.g = tempG;
                        item.f = item.g + item.h;
                        item.parent = current;
                        openList.insert(item);
                    }
                    else
                    {
                        Spot tmp = openList.get(item);
                        if(tmp.g > tempG)
                        {
                            tmp.update(tempG, current);                            
                        }
                        openList.insert(tmp);
                    }
                }

            }

            List<Spot> path = new List<Spot>();
            Spot help = current.parent;
            while (help != null)
            {
                path.Add(help);
                help = help.parent;

            }
            path.Reverse();
            Console.WriteLine(path.Count);
            foreach (var item in path)
            {
                Console.WriteLine(item.I + " " + item.J);
            }

        }

    static Spot[,] MakeReasonableGrid(int r, int c, int pacman_r, int pacman_c, int food_r, int food_c, String[] grid)
    {
        Spot[,] Grid = new Spot[r, c];
        int j_index = 0;
        foreach (string item in grid)
        {
            char[] helper = item.ToCharArray();
            for (int i = 0; i < helper.Length; i++)
            {
                if (helper[i].ToString() == "%")
                {
                    Grid[j_index, i] = new Spot(j_index, i, false);
                    continue;
                }
                if (helper[i].ToString() == "-")
                {
                    Grid[j_index, i] = new Spot(j_index, i, true);
                    continue;
                }
                else
                {
                    Grid[j_index, i] = new Spot(j_index, i, true);
                }
            }
            j_index++;
        }

            for (var i = 0; i < c; i++)
            {
                for (var j = 0; j < r; j++)
                {
                    if (Grid[j, i].accessible)
                    {
                        Grid[j, i].addNeighbors(Grid, c, r);
                    }
                }
            }

            return Grid;
        }

        static void Main(String[] args)
        {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
            int r, c;
            int pacman_r, pacman_c;
            int food_r, food_c;

            String pacman = Console.ReadLine();
            String food = Console.ReadLine();
            String pos = Console.ReadLine();

            String[] pos_split = pos.Split(' ');
            String[] pacman_split = pacman.Split(' ');
            String[] food_split = food.Split(' ');

            r = Convert.ToInt32(pos_split[0]);
            c = Convert.ToInt32(pos_split[1]);

            pacman_r = Convert.ToInt32(pacman_split[0]);
            pacman_c = Convert.ToInt32(pacman_split[1]);

            food_r = Convert.ToInt32(food_split[0]);
            food_c = Convert.ToInt32(food_split[1]);

            String[] grid = new String[r];

            for (int i = 0; i < r; i++)
            {
                grid[i] = Console.ReadLine();
            }


            astar(r, c, pacman_r, pacman_c, food_r, food_c, grid);
        }
    }
    }