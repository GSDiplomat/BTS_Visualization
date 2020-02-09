using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeSearch
{
    public class BinaryTreeNode: NotifyPropertyBase, IComparable
    {
        private int _nodeValue;
        private BinaryTreeNode _leftNode;
        private BinaryTreeNode _rightNode;
        private BinaryTreeNode _parentNode;

        public BinaryTreeNode(BinaryTreeNode _parentNode, int _nodeValue)
        {
            this._nodeValue = _nodeValue;
            this._parentNode = _parentNode;
        }

        public BinaryTreeNode ParentNode
        {
            get => _parentNode;
            set => value = _parentNode;
        }

        public int NodeValue
        {
            get => _nodeValue;
            set
            {
                if (value != _nodeValue)
                {
                    _nodeValue = value;
                    OnPropertyChanged("NodeValue");
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
                    OnPropertyChanged("LeftNode");
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
                    OnPropertyChanged("RightNode");
                }
            }
        }

        public override string ToString() => NodeValue.ToString();

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

            var treeNode = (BinaryTreeNode) obj;

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
    }
}
