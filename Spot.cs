using System;
using System.Collections.Generic;
using System.Text;

namespace astar
{
    class Spot
    {
        public override bool Equals(object obj)
        {
            var spot = obj as Spot;

            if (null != spot)
            {
                return I == spot.I && J == spot.J;
            }
            else
            {
                return base.Equals(obj);
            }
            
        }

        public override int GetHashCode()
        {
            int hash = 19;
            unchecked
            {
                hash = hash * 31 + I;
                hash = hash * 31 + J;
            }
            return hash;
        }

        public Spot(int i, int j, Boolean Accessible)
        {
            I = i;
            J = j;
            f = 0;
            g = 0;
            h = 0;
            neighbors = null;
            accessible = Accessible;
            discovered = false;
            parent = null;
            previous = null;
            next = null;
        }

        public void update(double cost, Spot Parent)
        {
            parent = Parent;
            g = cost;
            f = g + h;
        }

        public int I { get; set; }
        public int J { get; set; }
        public double f { get; set; }   //cost function
        public double h { get; set; }   // heuristics
        public double g { get; set; }   //cost
        public Queue<Spot> neighbors { get; set; }
        public Boolean accessible { get; set; }
        public Boolean discovered { get; set; }
        public Spot parent { get; set; }
        public Spot previous { get; set; }
        public Spot next { get; set; }

        public void addNeighbors(Spot[,] grid, int c, int r) => 
            neighbors = new Queue<Spot>(new List<Spot> { grid[I - 1, J], grid[I, J - 1], grid[I, J + 1], grid[I + 1, J] });        
    }

   
}
