using BinaryTreeSearch;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace BTSVisualization
{
    /// <summary>
    /// Логика взаимодействия для NodeView.xaml
    /// </summary>
    public partial class NodeView : UserControl
    {
        public BinaryTreeNode TreeNode { get; }
        public int Left { get; set; }
        public int Top { get; set; }

        LayoutNodeListener selfListener = new LayoutNodeListener();

        public NodeView(BinaryTreeNode node)
        {
            InitializeComponent();

            TreeNode = node;

            selfListener.CoordinatesChanged += ParentCoordinatesChanged;
            DataContext = TreeNode;
        }

        #region dp FrameworkElement ParentNode
        public FrameworkElement ParentNode
        {
            get { return (FrameworkElement)GetValue(ParentProperty); }
            set { SetValue(ParentProperty, value); }
        }

        public static readonly DependencyProperty ParentProperty =
            DependencyProperty.Register("ParentNode", typeof(FrameworkElement), typeof(NodeView),
                new FrameworkPropertyMetadata((sender, e) =>
                {
                    var self = (NodeView)sender;
                    ((NodeView)self.ParentNode).selfListener.AddTarget(self);
                }));
        #endregion

        public void SetCoordinates(int column, int row = 0)
        {
            Grid.SetColumn(this, column);
            Grid.SetRow(this, row);

            selfListener.RedrawChild();
        }

        public void AddToGrid(Grid grid)
        {
            grid.Children.Add(this);

            if (ParentNode != null)
                ((NodeView)ParentNode).selfListener.SetGridCoordinates(this);
        }

        private static void ParentCoordinatesChanged(object sender, EventArgs e)
        {
            var node = (NodeView)sender;

            if (node != null && node.ParentNode != null)
            {
                int row = 2 * (node.TreeNode.GetNodeDepth() - 1);
                int partWidth = 0;

                if (node.Parent != null)
                    partWidth = ((node.Parent as Grid).ColumnDefinitions.Count + 1) / (int)Math.Pow(2, node.TreeNode.GetNodeDepth());

                int column;

                if (node.TreeNode < ((NodeView)node.ParentNode).TreeNode)
                    column = Grid.GetColumn(node.ParentNode) - partWidth;
                else
                    column = Grid.GetColumn(node.ParentNode) + partWidth;

                node.SetCoordinates(column, row);
            }
        }
    }
}
