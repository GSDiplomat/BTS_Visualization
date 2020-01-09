using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeSearch
{
    public class BinaryTree
    {
        private int nodeValue;
        private int leftNode;
        private int rightNode;

        public BinaryTree(int nodeValue)
        {
            this.nodeValue = nodeValue;
        }

        public int NodeValue
        {
            get => nodeValue;
            set { nodeValue = value; }
        }

        public int LeftNode
        {
            get => leftNode;
            set { leftNode = value; }
        }

        public int RightNode
        {
            get => rightNode;
            set { rightNode = value; }
        }
    }
}
