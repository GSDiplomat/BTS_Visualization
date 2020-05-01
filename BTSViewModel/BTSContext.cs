using BinaryTreeSearch;
using System;
using System.Collections.Generic;
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

            PersonChangeListener.PropertyChanged += OnBTSInnerPropertyChanged;
        }

        public BinaryTreeEnvelope BinaryTreeEnvelope
        {
            get => _binaryTreeEnvelope;
            set
            {
                if (value != _binaryTreeEnvelope)
                {
                    _binaryTreeEnvelope = value;
                    OnPropertyChanged();
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
                    OnBTSPropertyChanged("BinaryTree");
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

        public int MyProperty { get; set; }

        public BTSCommand AddNodeCommand => _addNodeCommand ??
                                            (_addNodeCommand = new BTSCommand(
                                                obj =>
                                                {
#if DEBUG
                                                    if (((BinaryTreeNode)obj).NodeValue == 0)
                                                    {
                                                        List<int> Items = new List<int> { 9, 6, 17, 3, 8, 16, 20, 1, 4, 7, 12, 19, 21, 2, 5, 11, 14, 18, 10, 13, 15 };
                                                        foreach (var item in Items)
                                                        {
                                                            BinaryTree.AddNode(item);
                                                        }
                                                    }
                                                    else
#endif
                                                    BinaryTree.AddNode(((BinaryTreeNode)obj).NodeValue);

                                                },
                                                obj => true
                                            ));




        public BTSCommand RemoveNodeCommand => _removeNodeCommand ??
                                               (_removeNodeCommand = new BTSCommand(
                                                   obj =>
                                                   {
                                                       BinaryTree.RemoveNode(obj as BinaryTreeNode);
                                                   },
                                                   obj => obj != null
                                               ));

        public BTSCommand ClearTreeCommand => _clearTreeCommand ??
                                       (_clearTreeCommand = new BTSCommand(
                                           obj => BinaryTree = new BinaryTree()
                                       ));

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertName));
        }

        protected virtual void OnBTSPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            BinaryTreeEnvelope = new BinaryTreeEnvelope(_binaryTree, name);
        }

        protected void OnBTSInnerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BinaryTree"));
            BinaryTreeEnvelope = new BinaryTreeEnvelope(_binaryTree, e.PropertyName);
        }
    }
}
