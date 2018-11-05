using Dictionary.AVLTree;
using Dictionary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.HashTable
{
    class HashTable<T> : Dictionary<T, Node<T>>, SearchStruct<T>
    {
        public HashTable()
        {
        }

        public void Add(T data, int reference)
        {
            base.Add(data, new Node<T>(data, reference));
        }

        public int? Find(T key)
        {
            try
            {
                return base[key].reference;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var el in Keys)
                sb.Append($"{el}\n");
            return sb.ToString();
        }
    }
}
