namespace BinaryTreeSearch
{
    public class BinaryTree : NotifyPropertyBase
    {
        private BinaryTreeNode _root;
        private int _nodeCount;

        public BinaryTreeNode Root
        {
            get => _root;
            set
            {
                if (value != _root)
                {
                    _root = value;
                    OnPropertyChanged("Root");
                }
            }
        }

        public int NodeCount => _nodeCount;

        public void AddNode(int nodeValue)
        {
            _nodeCount++;

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
            if (nodeValue < root.NodeValue)
            {
                if (root.LeftNode == null)
                {
                    root.LeftNode = new BinaryTreeNode(nodeValue);
                }
                else
                {
                    AddNode(root.LeftNode, nodeValue);
                }
            }
            else
            {
                if (root.RightNode == null)
                {
                    root.RightNode = new BinaryTreeNode(nodeValue);
                }
                else
                {
                    AddNode(root.RightNode, nodeValue);
                }
            }
        }

        public string Traversal(eTraversalType traversalType)
        {
            var sequenceNodes = string.Empty;

            switch (traversalType)
            {
                case eTraversalType.Preorder:
                    TraversalPreorder(Root, ref sequenceNodes);
                    break;
                case eTraversalType.Inorder:
                    TraversalInorder(Root, ref sequenceNodes);
                    break;
                case eTraversalType.Postorder:
                    TraversalPostorder(Root, ref sequenceNodes);
                    break;
            }

            return sequenceNodes;
        }

        private void TraversalPreorder(BinaryTreeNode node, ref string SequenceNodes)
        {
            if (node == null)
                return;

            SequenceNodes += " " + node;

            TraversalPreorder(node.LeftNode, ref SequenceNodes);
            TraversalPreorder(node.RightNode, ref SequenceNodes);
        }

        private void TraversalInorder(BinaryTreeNode node, ref string SequenceNodes)
        {
            if (node == null)
                return;

            TraversalInorder(node.LeftNode, ref SequenceNodes);

            SequenceNodes += " " + node;

            TraversalInorder(node.RightNode, ref SequenceNodes);
        }

        private void TraversalPostorder(BinaryTreeNode node, ref string SequenceNodes)
        {
            if (node == null)
                return;

            TraversalPostorder(node.LeftNode, ref SequenceNodes);
            TraversalPostorder(node.RightNode, ref SequenceNodes);

            SequenceNodes += " " + node;
        }
    }
}
