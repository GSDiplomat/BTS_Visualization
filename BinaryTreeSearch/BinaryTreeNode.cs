using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeSearch
{
    public class BinaryTreeNode : NotifyPropertyBase, IComparable
    {
        private int _nodeValue;
        private BinaryTreeNode _leftNode;
        private BinaryTreeNode _rightNode;
        private BinaryTreeNode _parentNode;

        public BinaryTreeNode(BinaryTreeNode parentNode, int nodeValue)
        {
            _nodeValue = nodeValue;
            ParentNode = parentNode;
        }

        //TODO: Сделать свойством
        public BinaryTreeNode ParentNode;

        public int NodeValue
        {
            get => _nodeValue;
            set
            {
                if (value != _nodeValue)
                {
                    _nodeValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public BinaryTreeNode LeftNode
        {
            get => _leftNode;
            set
            {
                if (value != _leftNode)
                {
                    _leftNode = value;
                    OnPropertyChanged();
                }
            }
        }

        public BinaryTreeNode RightNode
        {
            get => _rightNode;
            set
            {
                if (value != _rightNode)
                {
                    _rightNode = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NodePath => GetNodePath();

        public int GetNodeDepth() => NodePath.Split('.').Length;

        private string GetNodePath()
        {
            BinaryTreeNode binaryTreeNode = this;

            string reverseNodePath = string.Empty;
            string nodePath;

            while (binaryTreeNode.ParentNode != null)
            {
                if (binaryTreeNode > binaryTreeNode.ParentNode)
                    reverseNodePath += "RightNode.";
                else
                    reverseNodePath += "LeftNode.";

                binaryTreeNode = binaryTreeNode.ParentNode;
            }

            reverseNodePath += "Root";

            var splitRnPath = reverseNodePath.Split('.').Reverse();

            nodePath = splitRnPath.First();

            foreach (var splitPath in splitRnPath.Skip(1))
            {
                nodePath += "." + splitPath;
            }

            return nodePath;
        }


        public override string ToString() => NodeValue.ToString();

        #region IComparable
        public static bool operator <(BinaryTreeNode firstNode, BinaryTreeNode secondNode)
        {
            return (firstNode.CompareTo(secondNode) < 0);
        }

        public static bool operator >(BinaryTreeNode firstNode, BinaryTreeNode secondNode)
        {
            return (firstNode.CompareTo(secondNode) > 0);
        }

        public static bool operator <=(BinaryTreeNode firstNode, BinaryTreeNode secondNode)
        {
            return (firstNode.CompareTo(secondNode) <= 0);
        }

        public static bool operator >=(BinaryTreeNode firstNode, BinaryTreeNode secondNode)
        {
            return (firstNode.CompareTo(secondNode) >= 0);
        }

        public int CompareTo(object obj)
        {
            if (!(obj is BinaryTreeNode)) throw new ArgumentException();

            var treeNode = (BinaryTreeNode)obj;

            if (NodeValue > treeNode.NodeValue)
            {
                return 1;
            }

            if (NodeValue < treeNode.NodeValue)
            {
                return -1;
            }

            return 0;
        }
        #endregion
    }
}
