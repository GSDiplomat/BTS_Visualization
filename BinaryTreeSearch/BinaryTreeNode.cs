using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeSearch
{
    public class BinaryTreeNode: NotifyPropertyBase
    {
        private int _nodeValue;
        private BinaryTreeNode _leftNode;
        private BinaryTreeNode _rightNode;

        public BinaryTreeNode(int nodeValue)
        {
            _nodeValue = nodeValue;
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
    }
}
