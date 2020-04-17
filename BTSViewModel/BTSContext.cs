using BinaryTreeSearch;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace BTSViewModel
{
    public class BTSContext : NotifyPropertyBase
    {
        private BinaryTree _binaryTree;
        private BTSCommand _addNodeCommand;
        private BTSCommand _removeNodeCommand;
        private BinaryTreeNode _currentNode = new BinaryTreeNode(null, 0);

        public BTSContext()
        {
            _binaryTree = new BinaryTree();

            var personChangeListener = ChangeListener.Create(_binaryTree);

            personChangeListener.PropertyChanged += new PropertyChangedEventHandler(value_PropertyChanged);
        }

        private void value_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("Changed Property: " + e.PropertyName);
        }

        public BinaryTree BinaryTree
        {
            get => _binaryTree;
            set
            {
                if (value != _binaryTree)
                {
                    _binaryTree = value;
                    OnPropertyChanged();
                }
            }
        }

        public BinaryTreeNode CurrentNode
        {
            get => _currentNode;
            set
            {
                if (value != _currentNode)
                {
                    _currentNode = value;
                    OnPropertyChanged();
                }
            }
        }

        public BTSCommand AddNodeCommand => _addNodeCommand ??
                                            (_addNodeCommand = new BTSCommand(
                                                obj =>
                                                {
                                                    BinaryTree.AddNode(((BinaryTreeNode)obj).NodeValue);
                                                },
                                                obj => true
                                            ));


        public BTSCommand RemoveNodeCommand => _removeNodeCommand ??
                                               (_removeNodeCommand = new BTSCommand(
                                                   obj => BinaryTree.RemoveNode(obj as BinaryTreeNode),
                                                   obj => obj != null
                                               ));

    }
}
