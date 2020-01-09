using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeSearch
{
    public class BinaryTreeNode
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
            set { nodeValue = value; }
        }

        public BinaryTreeNode LeftNode
        {
            get => leftNode;
            set { leftNode = value; }
        }

        public BinaryTreeNode RightNode
        {
            get => rightNode;
            set { rightNode = value; }
        }

        public void AddNode(int nodeValue)
        {
            //TODO:BorisoglebskiyIK - develop method wihch add the node to BinaryTree
        }
    }
}
