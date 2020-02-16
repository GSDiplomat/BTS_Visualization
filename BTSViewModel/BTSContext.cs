using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryTreeSearch;

namespace BTSViewModel
{
    public class BTSContext: NotifyPropertyBase
    {
        private BinaryTree _binaryTree;

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
    }
}
