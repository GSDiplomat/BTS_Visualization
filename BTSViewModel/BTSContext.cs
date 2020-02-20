using BinaryTreeSearch;

namespace BTSViewModel
{
    public class BTSContext : NotifyPropertyBase
    {
        private BinaryTree _binaryTree;
        private BTSCommand _addNodeCommand;
        private BTSCommand _removeNodeCommand;

        public BTSContext()
        {
            _binaryTree = new BinaryTree();
        }

        public BinaryTree BinaryTree
        {
            get => _binaryTree;
            set
            {
                if (value != _binaryTree)
                {
                    _binaryTree = value;
                    OnPropertyChanged("BinaryTree");
                }
            }
        }

        public BTSCommand AddNodeCommand => _addNodeCommand ??
                                            (_addNodeCommand = new BTSCommand(
                                                obj =>
                                                {
                                                    BinaryTree.AddNode((int)obj);
                                                },
                                                obj => obj != null
                                            ));


        public BTSCommand RemoveNodeCommand => _removeNodeCommand ??
                                               (_removeNodeCommand = new BTSCommand(
                                                   obj => BinaryTree.RemoveNode(obj as BinaryTreeNode),
                                                   obj => obj != null
                                               ));

    }
}
