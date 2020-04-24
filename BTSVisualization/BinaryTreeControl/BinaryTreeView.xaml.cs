using BinaryTreeSearch;
using BTSViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BTSVisualization
{
    /// <summary>
    /// Логика взаимодействия для BinaryTreeView.xaml
    /// </summary>
    public partial class BinaryTreeView : UserControl
    {
        public BinaryTreeView()
        {
            InitializeComponent();
        }

        public BinaryTreeEnvelope BinaryTreeData
        {
            get { return (BinaryTreeEnvelope)GetValue(BinaryTreeProperty); }
            set 
            { 
                SetValue(BinaryTreeProperty, value);
            }
        }

        public static readonly DependencyProperty BinaryTreeProperty =
            DependencyProperty.Register("BinaryTreeData", typeof(BinaryTreeEnvelope), typeof(BinaryTreeView), new PropertyMetadata(new BinaryTreeEnvelope(), BinaryTreeChanged));


        private static void BinaryTreeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var binaryTreeEnvelope = (BinaryTreeEnvelope)e.NewValue;
            ChangedProperty changedProperty;
            BinaryTreeNode binaryTreeNode;

            BinaryTreeDrawer.BinaryTreeGrid = (sender as BinaryTreeView).TreeGrid;

            if (binaryTreeEnvelope.BinaryTree != null)
            {
                changedProperty = new ChangedProperty(binaryTreeEnvelope.BinaryTree, binaryTreeEnvelope.PropertyName);

                if (changedProperty.PropertyName == "MaxDepth")
                {
                    BinaryTreeDrawer.ChangeGrid(ChangedProperty.OldMaxDepth, changedProperty.NewMaxDepth);
                    ChangedProperty.OldMaxDepth = changedProperty.NewMaxDepth;
                }
                else if(changedProperty.PropertyName == "BinaryTree")
                {
                    BinaryTreeDrawer.ClearTree();
                }
                else
                {
                    binaryTreeNode = ChangedProperty.NodeReferences[changedProperty.PropertyName];

                    BinaryTreeDrawer.AddNode(binaryTreeNode);
                }
            }
        }


    }
}
