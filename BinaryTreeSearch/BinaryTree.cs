using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeSearch
{
    public class BinaryTree: NotifyPropertyBase
    {
        private BinaryTreeNode root;
        private int nodeCount;

        public BinaryTreeNode Root
        {
            get => root;
            set
            {
                if (value != root)
                {
                    root = value;
                    OnPropertyChanged("Root");
                }
            }
        }

        public int NodeCount => nodeCount;

        public void AddNode(int nodeValue)
        {
            nodeCount++;

            if (Root == null)
            {
                Root = new BinaryTreeNode(nodeValue);
            }
            else
            {
                AddNode(Root, nodeValue);
            }
        }

        private void AddNode(BinaryTreeNode root, int nodeValue)
        {
            if (nodeValue < Root.NodeValue)
            {
                if (Root.LeftNode == null)
                {
                    Root.LeftNode = new BinaryTreeNode(nodeValue);
                }
                else
                {
                    AddNode(Root.LeftNode, nodeValue);
                }
            }
            else
            {
                if (Root.RightNode == null)
                {
                    Root.RightNode = new BinaryTreeNode(nodeValue);
                }
                else
                {
                    AddNode(Root.RightNode, nodeValue);
                }
            }
        }
    }
}
