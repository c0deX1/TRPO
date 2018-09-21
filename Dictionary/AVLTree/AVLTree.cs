using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dictionary.AVLTree
{
    class AVLTree<T>
    {
        Node<T> root;

        public AVLTree()
        {
        }

        public void Add(T data)
        {
            Node<T> newElement = new Node<T>(data);
            root = (root == null) ? newElement : RecursiveInsert(root, newElement);
        }

        private Node<T> RecursiveInsert(Node<T> current, Node<T> newElement)
        {
            if (current == null)
            {
                current = newElement;
                return current;
            }
            else if (newElement.data.GetHashCode() < current.data.GetHashCode())
            {
                current.left = RecursiveInsert(current.left, newElement);
                current = Balance(current);
            }
            else if (newElement.data.GetHashCode() > current.data.GetHashCode())
            {
                current.right = RecursiveInsert(current.right, newElement);
                current = Balance(current);
            }
            return current;
        }

        private Node<T> Balance(Node<T> current)
        {
            int b_factor = BalanceNumber(current);
            if (b_factor > 1)
            {
                if (BalanceNumber(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (BalanceNumber(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }

        public void Delete(T target)
        {//and here
            root = Delete(root, target);
        }
        private Node<T> Delete(Node<T> current, T target)
        {
            Node<T> parent;
            if (current == null)
            { return null; }
            else
            {
                if (target.GetHashCode() < current.data.GetHashCode())
                {
                    current.left = Delete(current.left, target);
                    if (BalanceNumber(current) == -2)//here
                    {
                        if (BalanceNumber(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                else if (target.GetHashCode() > current.data.GetHashCode())
                {
                    current.right = Delete(current.right, target);
                    if (BalanceNumber(current) == 2)
                    {
                        if (BalanceNumber(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                else
                {
                    if (current.right != null)
                    {
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);
                        if (BalanceNumber(current) == 2)
                        {
                            if (BalanceNumber(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.left;
                    }
                }
            }
            return current;
        }
        public void Find(T key)
        {
            if (Find(key, root).data.Equals(key))
            {
                Console.WriteLine("{0} was found!", key);
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }
        }
        private Node<T> Find(T target, Node<T> current)
        {

            if (target.GetHashCode() < current.data.GetHashCode())
            {
                if (target.Equals(current.data))
                {
                    return current;
                }
                else
                    return Find(target, current.left);
            }
            else
            {
                if (target.Equals(current.data))
                {
                    return current;
                }
                else
                    return Find(target, current.right);
            }

        }
        public string DisplayTree()
        {
            StringBuilder sb = new StringBuilder();
            if (root == null)
            {
                return sb.Append("Tree is empty").ToString();
            }
            return InOrderDisplayTree(root, sb).ToString();
        }
        private StringBuilder InOrderDisplayTree(Node<T> current, StringBuilder sb)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.left, sb);
                sb.Append($"{ current.data} ");
                InOrderDisplayTree(current.right, sb);
            }
            return sb;
        }
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int getHeight(Node<T> current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int BalanceNumber(Node<T> current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node<T> RotateRR(Node<T> parent)
        {
            Node<T> pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node<T> RotateLL(Node<T> parent)
        {
            Node<T> pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node<T> RotateLR(Node<T> parent)
        {
            Node<T> pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node<T> RotateRL(Node<T> parent)
        {
            Node<T> pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}