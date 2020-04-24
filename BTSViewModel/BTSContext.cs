using BinaryTreeSearch;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BTSViewModel
{
    public class BTSContext : INotifyPropertyChanged
    {
        private BinaryTreeEnvelope _binaryTreeEnvelope = new BinaryTreeEnvelope();
        private BinaryTree _binaryTree = new BinaryTree();
        private BTSCommand _addNodeCommand;
        private BTSCommand _removeNodeCommand;
        private BTSCommand _clearTreeCommand;
        private BinaryTreeNode _currentNode = new BinaryTreeNode(null, 0);

        public ChangeListener PersonChangeListener;

        public BTSContext()
        {
            PersonChangeListener = ChangeListener.Create(BinaryTree);

            PersonChangeListener.PropertyChanged += OnInnerPropertyChanged;
        }


        public BinaryTreeEnvelope BinaryTreeEnvelope
        {
            get => _binaryTreeEnvelope;
            set
            {
                if (value != _binaryTreeEnvelope)
                {
                    _binaryTreeEnvelope = value;
                    ChangingProperties();
                }
            }
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


        public BinaryTreeNode CurrentNode
        {
            get => _currentNode;
            set
            {
                if (value != _currentNode)
                {
                    _currentNode = value;
                    OnPropertyChanged("CurrentNode");
                }
            }
        }

        public int MyProperty { get; set; }

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

        public BTSCommand ClearTreeCommand => _clearTreeCommand ??
                                       (_clearTreeCommand = new BTSCommand(
                                           obj => BinaryTree = new BinaryTree()
                                       ));

        public event PropertyChangedEventHandler PropertyChanged;

        private void ChangingProperties()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BinaryTreeEnvelope"));
        }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            BinaryTreeEnvelope = new BinaryTreeEnvelope(_binaryTree, name);
        }

        protected void OnInnerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BinaryTree"));
            BinaryTreeEnvelope = new BinaryTreeEnvelope(_binaryTree, e.PropertyName);
        }
    }
}
