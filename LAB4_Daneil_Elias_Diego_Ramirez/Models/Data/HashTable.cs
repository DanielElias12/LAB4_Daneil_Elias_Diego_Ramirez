using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB4_Daneil_Elias_Diego_Ramirez.Models.Data
{
    public class HashNode<T>
    {
        public HashNode<T> Next { get; set; }

        public string Key { get; set; }

        public T Value { get; set; }
    }
    public class HashTable<T>
    {
        private readonly HashNode<T>[] buckets;

        public HashTable(int size)
        {
            buckets = new HashNode<T>[size];
        }

        public void Add(string key, T item)
        {
            ValidateKey(key);

            var valueNode = new HashNode<T> { Key = key, Value = item, Next = null };
            int position = GetBucketByKey(key);
            HashNode<T> listNode = buckets[position];

            if (null == listNode)
            {
                buckets[position] = valueNode;
            }
            else
            {
                while (null != listNode.Next)
                {
                    listNode = listNode.Next;
                }
                listNode.Next = valueNode;
            }
        }


        public T GetNode(string key)
        {
            ValidateKey(key);

            var (_, node) = GetNodeByKey(key);

            if (node == null) throw
                  new ArgumentOutOfRangeException(nameof(key), $"The key '{key}' is not found");
            return node.Value;
        }

        public bool Remove(string key)
        {
            ValidateKey(key);
            int position = GetBucketByKey(key);

            var (previous, current) = GetNodeByKey(key);

            if (null == current) return false;

            if (null == previous)
            {
                buckets[position] = null;
                return true;
            }
            previous.Next = current.Next;
            return true;
        }

        public int GetBucketByKey(string key)
        {
            return key[0] % buckets.Length;
        }
        protected (HashNode<T> previous, HashNode<T> current) GetNodeByKey(string key)
        {
            int position = GetBucketByKey(key);
            HashNode<T> listNode = buckets[position];
            HashNode<T> previous = null;

            while (null != listNode)
            {
                if (listNode.Key == key)
                {
                    return (previous, listNode);
                }
                previous = listNode;
                listNode = listNode.Next;
            }
            return (null, null);

        }

        protected void ValidateKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
        }
    }
}
