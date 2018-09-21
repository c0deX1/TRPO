using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dictionary.AVLTree
{
    class Node<T>
    {
        public T data;
        public Node<T> left;
        public Node<T> right;

        public Node(T data)
        {
            this.data = data;
        }

        public override bool Equals(object obj)
        {
            var node = obj as Node<T>;
            return node != null &&
                   EqualityComparer<T>.Default.Equals(data, node.data) &&
                   EqualityComparer<Node<T>>.Default.Equals(left, node.left) &&
                   EqualityComparer<Node<T>>.Default.Equals(right, node.right);
        }

        public override int GetHashCode()
        {
            var hashCode = 1893063498;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(data);
            return hashCode;
        }
    }
}