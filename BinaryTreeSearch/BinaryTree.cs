namespace BinaryTreeSearch
{
    public enum eTraversalType
    {
        Preorder,
        Inorder,
        Postorder
    }

    public class BinaryTree : NotifyPropertyBase
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
            string SequenceNodes = string.Empty;

            switch (traversalType)
            {
                case eTraversalType.Preorder:
                    TraversalPreorder(Root, ref SequenceNodes);
                    break;
                case eTraversalType.Inorder:
                    TraversalInorder(Root, ref SequenceNodes);
                    break;
                case eTraversalType.Postorder:
                    TraversalPostorder(Root, ref SequenceNodes);
                    break;
                default:
                    break;
            }

            return SequenceNodes;
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
