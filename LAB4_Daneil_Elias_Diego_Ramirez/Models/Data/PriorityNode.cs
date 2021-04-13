using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB4_Daneil_Elias_Diego_Ramirez.Models.Data
{
    public class PriorityNode<T>
    {
        public PriorityNode<T> Next { get; set; }
        public int prioridad;
        public T Data { get; set; }
    }
}
