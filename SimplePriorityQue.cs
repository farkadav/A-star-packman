using System;
using System.Collections.Generic;
using System.Text;

namespace astar
{
    class SimplePriorityQue
    {
        public Spot first { get; set; }
        public Spot last { get; set; }
        public int length { get; set; }



        public SimplePriorityQue()
        {
            first = null;
            last = null;
        }

        public Boolean isEmpty()
        {
            return length == 0;
        }

        public Boolean insert(Spot x)
        {
            if (x == null) return false;
            if (length == 0)
            {
                first = x;
                last = x;
            }
            else
            {
                if (x.f <= first.f)
                {
                    first.previous = x;
                    x.next = first;
                    x.previous = null;
                    first = x;
                }
                else
                {
                    Spot tmp = first.next;
                    while (tmp != null)
                    {
                        if (x.f <= tmp.f)
                        {
                            x.previous = tmp.previous;
                            x.next = tmp;
                            tmp.previous.next = x;
                            tmp.previous = x;
                            break;
                        }
                        tmp = tmp.next;
                    }
                    if (tmp == null)
                    {
                        x.previous = last;
                        last.next = x;
                        last = x;
                    }
                }
            }
            length++;
            return true;
                        
        }

        public Spot getElement()
        {
            if (first == null) return null;
            else
            {
                length--;
                Spot tmp = first;
                if(tmp.next == null)
                {
                    last = null;
                    return tmp;
                }
                else
                {
                    first = tmp.next;
                    first.previous = null;
                    return tmp;
                }
            }
        }

        public Boolean contains(Spot x)
        {
            Spot tmp = first;
            while (tmp != null)
            {
                if (tmp.Equals(x)) return true;
                tmp = tmp.next;
                
            }
            return false;
        }

        public Spot get(Spot x)
        {
            Spot tmp = first;
            while (tmp != null)
            {
                if (tmp.Equals(x))
                {
                    if (tmp.previous != null) tmp.previous.next = tmp.next;
                    else first = tmp.next;

                    if (tmp.next != null) tmp.next.previous = tmp.previous;
                    else last = tmp.previous;

                    length--;
                    return tmp;
                }
                tmp = tmp.next;

            }
            return null;
        }

      
    }
}
