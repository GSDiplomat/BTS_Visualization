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
        private static BinaryTreeDrawer TreeDrawer = new BinaryTreeDrawer();

        public BinaryTreeView()
        {
            InitializeComponent();
        }

        public BinaryTreeEnvelope BinaryTreeData
        {
            get { return (BinaryTreeEnvelope)GetValue(BinaryTreeProperty); }
            set { SetValue(BinaryTreeProperty, value); }
        }

        public BinaryTreeNode SelectedNode
        {
            get { return (BinaryTreeNode)GetValue(SelectedNodeProperty); }
            set { SetValue(SelectedNodeProperty, value); }
        }

        public static readonly DependencyProperty BinaryTreeProperty =
            DependencyProperty.Register("BinaryTreeData", typeof(BinaryTreeEnvelope), typeof(BinaryTreeView), new PropertyMetadata(new BinaryTreeEnvelope(), OnBinaryTreeChanged));
        public static readonly DependencyProperty SelectedNodeProperty =
            DependencyProperty.Register("SelectedNode", typeof(BinaryTreeNode), typeof(BinaryTreeView), new PropertyMetadata(new BinaryTreeNode(null, 0), OnSelectedNodeChanged));


        private static void OnBinaryTreeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var binaryTreeEnvelope = (BinaryTreeEnvelope)e.NewValue;
            ChangedProperty changedProperty;
            BinaryTreeNode binaryTreeNode;

            TreeDrawer.BinaryTreeGrid = (sender as BinaryTreeView).TreeGrid;

            if (binaryTreeEnvelope.BinaryTree != null)
            {
                changedProperty = new ChangedProperty(binaryTreeEnvelope.BinaryTree, binaryTreeEnvelope.PropertyName);

                TreeDrawer.GridRowCounter = changedProperty.NewMaxDepth;

                if (changedProperty.PropertyName == "BinaryTree")
                {
                    TreeDrawer.ClearTree();
                }
                else if (changedProperty.PropertyName.Contains("Root") && !changedProperty.PropertyName.Contains("NodeValue"))
                {
                    binaryTreeNode = ChangedProperty.NodeReferences[changedProperty.PropertyName];
                            TreeDrawer.AddNode(binaryTreeNode);
                }
            }
        }

        static void OnSelectedNodeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void BTSNodeViewSelect(object sender, MouseButtonEventArgs e)
        {
            var oldSelectedNode = TreeDrawer.BinaryTreeGrid.Children.Cast<BTSNodeView>().FirstOrDefault(item => item.IsSelected == true);

            if (oldSelectedNode != null)
            {
                NodeFocus(oldSelectedNode);
            }

            SelectedNode = (sender as BTSNodeView).Node;

            NodeFocus(sender as BTSNodeView);
        }

        private void NodeFocus(BTSNodeView nodeView)
        {
            if (nodeView.IsSelected)
            {
                nodeView.IsSelected = false;
                nodeView.ellipseNode.Stroke = new SolidColorBrush(Colors.Black);
            }
            else
            {
                nodeView.IsSelected = true;
                nodeView.ellipseNode.Stroke = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
