using BinaryTreeSearch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BTSVisualization
{
    public class ChangedProperty
    {
        public static Dictionary<string, BinaryTreeNode> NodeReferences = new Dictionary<string, BinaryTreeNode>();

        public static int OldMaxDepth = 1;
        public static BinaryTreeNode OldTreeNode;

        public int NewMaxDepth;
        public BinaryTreeNode NewTreeNode;
         
        public ChangedProperty(BinaryTree _binaryTree, string propertyName)
        {
            NewMaxDepth = _binaryTree.MaxDepth;

            PropertyName = propertyName;

            if (PropertyName != "MaxDepth" && PropertyName != "BinaryTree")
                NodeReferences.Add(propertyName, GetNode(_binaryTree));
        }

        public string PropertyName { get; set; }

        private BinaryTreeNode GetNode(BinaryTree binaryTree)
        {
            if (PropertyName == "Root")
            {
                return binaryTree.Root;
            }

            return GetNode(binaryTree.Root, PropertyName.Split(new char[] { '.' }, 2).Last());
        }

        private BinaryTreeNode GetNode(BinaryTreeNode node, string fullPropertyName)
        {
            var nodeChain = fullPropertyName.Split(new char[] { '.' }, 2);

            if (nodeChain.Length == 1)
                return (BinaryTreeNode)typeof(BinaryTreeNode).GetProperty(nodeChain.Last()).GetValue(node);

            string nodeName = nodeChain.First();

            return GetNode((BinaryTreeNode)typeof(BinaryTreeNode).GetProperty(nodeName).GetValue(node), nodeChain.Last());
        }

    }
}
