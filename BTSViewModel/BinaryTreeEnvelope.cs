using BinaryTreeSearch;

namespace BTSViewModel
{
    public class BinaryTreeEnvelope
    {
        public BinaryTreeEnvelope() { }

        public BinaryTreeEnvelope(BinaryTree _binaryTree, string propertyName)
        {
            BinaryTree = _binaryTree;

            PropertyName = propertyName;
        }

        public BinaryTree BinaryTree { get; set; }

        public string PropertyName { get; set; }
    }
}
