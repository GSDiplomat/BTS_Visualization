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
                Root = new BinaryTreeNode(null, nodeValue);
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
                    root.LeftNode = new BinaryTreeNode(root, nodeValue);
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
                    root.RightNode = new BinaryTreeNode(root, nodeValue);
                }
                else
                {
                    AddNode(root.RightNode, nodeValue);
                }
            }
        }

        public void RemoveNode(BinaryTreeNode node)
        {
            if (node.ParentNode == null)
            {
                Root = node.LeftNode;

                if (Root != null)
                {
                    Root.ParentNode = null;
                }
            }

            if (node.RightNode == null)
            {
                if (node.ParentNode != null)
                {
                    if (node.ParentNode > node)
                    {
                        node.ParentNode.LeftNode = node.LeftNode;
                    }
                    else
                    {
                        node.ParentNode.RightNode = node.LeftNode;
                    }
                }
            }
            else if (node.RightNode.LeftNode == null)
            {
                node.RightNode.LeftNode = node.LeftNode;

                if (node.ParentNode != null)
                {
                    if (node.ParentNode > node)
                    {
                        node.ParentNode.LeftNode = node.RightNode;
                    }
                    else
                    {
                        node.ParentNode.RightNode = node.RightNode;
                    }
                }
            }
            else
            {
                var leftBottomNode = node.RightNode.LeftNode;

                while (leftBottomNode.LeftNode != null)
                {
                    leftBottomNode = leftBottomNode.LeftNode;
                }

                leftBottomNode.ParentNode.LeftNode = leftBottomNode.RightNode;

                leftBottomNode.LeftNode = node.LeftNode;
                leftBottomNode.RightNode = node.RightNode;

                if (node.ParentNode != null)
                {
                    if (node.ParentNode > node)
                    {
                        node.ParentNode.LeftNode = leftBottomNode;
                    }
                    else
                    {
                        node.ParentNode.RightNode = leftBottomNode;
                    }
                }
            }
        }

        private BinaryTreeNode FindNode(int nodeValue)
        {
            BinaryTreeNode nodeSearch = Root;

            while (nodeSearch != null)
            {
                if (nodeSearch.NodeValue < nodeValue)
                {
                    nodeSearch = nodeSearch.LeftNode;
                }
                else if (nodeSearch.NodeValue >= nodeValue)
                {
                    nodeSearch = nodeSearch.RightNode;
                }
                else
                {
                    break;
                }
            }

            return nodeSearch;
        }

        #region Traversal of binary tree
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

        private void TraversalPreorder(BinaryTreeNode node, ref string sequenceNodes)
        {
            if (node == null)
                return;

            sequenceNodes += " " + node;

            TraversalPreorder(node.LeftNode, ref sequenceNodes);
            TraversalPreorder(node.RightNode, ref sequenceNodes);
        }

        private void TraversalInorder(BinaryTreeNode node, ref string sequenceNodes)
        {
            if (node == null)
                return;

            TraversalInorder(node.LeftNode, ref sequenceNodes);

            sequenceNodes += " " + node;

            TraversalInorder(node.RightNode, ref sequenceNodes);
        }

        private void TraversalPostorder(BinaryTreeNode node, ref string sequenceNodes)
        {
            if (node == null)
                return;

            TraversalPostorder(node.LeftNode, ref sequenceNodes);
            TraversalPostorder(node.RightNode, ref sequenceNodes);

            sequenceNodes += " " + node;
        }
        #endregion
    }
}
