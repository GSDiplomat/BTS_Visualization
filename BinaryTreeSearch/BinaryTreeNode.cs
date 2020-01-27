using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeSearch
{
    public class BinaryTreeNode: NotifyPropertyBase
    {
        private int nodeValue;
        private BinaryTreeNode leftNode;
        private BinaryTreeNode rightNode;

        public BinaryTreeNode(int nodeValue)
        {
            this.nodeValue = nodeValue;
        }

        public int NodeValue
        {
            get => nodeValue;
            set
            {
                if (value != nodeValue)
                {
                    nodeValue = value;
                    OnPropertyChanged("NodeValue");
                }
            }
        }

        public BinaryTreeNode LeftNode
        {
            get => leftNode;
            set
            {
                if (value != leftNode)
                {
                    leftNode = value;
                    OnPropertyChanged("LeftNode");
                }
            }
        }

        public BinaryTreeNode RightNode
        {
            get => rightNode;
            set
            {
                if (value != rightNode)
                {
                    rightNode = value;
                    OnPropertyChanged("RightNode");
                }
            }
        }

        public override string ToString()
        {
            return NodeValue.ToString();
        }
    }
}
