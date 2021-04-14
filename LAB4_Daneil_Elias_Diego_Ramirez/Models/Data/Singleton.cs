using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB4_Daneil_Elias_Diego_Ramirez.Models.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();

        public HashTable<Task> TaskHashtable;
        public PriorityQueue<Task> TaskIndex;
        public List<Developer> DevelopersList;
        public List<Task> VisibleTasks;
        private Singleton()
        {
            
            TaskIndex = new PriorityQueue<Task>();
            TaskHashtable = new HashTable<Task>(10);
            DevelopersList = new List<Developer>();
            VisibleTasks = new List<Task>();

        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}

