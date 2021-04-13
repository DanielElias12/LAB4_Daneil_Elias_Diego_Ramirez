using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB4_Daneil_Elias_Diego_Ramirez.Models.Data
{
    public class PriorityQueue<T>
    {
        private PriorityNode<T> cola;

        public PriorityQueue()
        {
            cola = new PriorityNode<T>();
            cola.Next = null;
        }



        public void insert(T data, int priority)
        {
            PriorityNode<T> current;
            PriorityNode<T> newnode;

            Boolean found = false;
            current = cola;
            while ((current.Next != null) && (!found))
            {
                if (current.Next.prioridad > priority)
                {
                    found = true;
                }
                else
                {
                    current = current.Next;
                }
            }
            newnode = current.Next;
            current.Next = new PriorityNode<T>();
            current = current.Next;
            current.Data = data;
            current.prioridad = priority;
            current.Next = newnode;
        }
    }
}
