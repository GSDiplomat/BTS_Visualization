using BinaryTreeSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace BTSVisualization
{
    public class BinaryTreeDrawer
    {
        private int _gridRowCounter = 1;
        private NodeView _rootView;

        public Grid BinaryTreeGrid;
        public int RootColumn;

        private Dictionary<int, int> DepthDelta = new Dictionary<int, int>();

        public NodeView RootView => _rootView ?? (_rootView = BinaryTreeGrid.Children.Cast<NodeView>().FirstOrDefault(item => Grid.GetRow(item) == 0));

        #region MaxDepthChanged
        public int GridRowCounter
        {
            set
            {
                if (value > _gridRowCounter)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        BinaryTreeGrid.RowDefinitions.Add(new RowDefinition());
                    }
                    for (int i = 1; i < Math.Pow(2, value); i += 2)
                    {
                        BinaryTreeGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                }
                else if (value < _gridRowCounter)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        BinaryTreeGrid.RowDefinitions.Remove(BinaryTreeGrid.RowDefinitions.Last());
                    }

                    for (int i = 1; i < Math.Pow(2, value); i += 2)
                    {
                        BinaryTreeGrid.ColumnDefinitions.Remove(BinaryTreeGrid.ColumnDefinitions.Last());
                    }
                }

                if (value != _gridRowCounter)
                {
                    _gridRowCounter = value;

                    RootView.SetCoordinates((BinaryTreeGrid.ColumnDefinitions.Count) / 2);
                }
            }
        }
        #endregion

        #region NodeAdded
        public void AddNode(BinaryTreeNode node)
        {
            NodeView nodeView = new NodeView(node);

            if (node.ParentNode != null)
                nodeView.ParentNode = GetNodeView(node.ParentNode);


            if (node.ParentNode == null)
            {
                nodeView.SetCoordinates(1);
                DepthDelta.Add(1, 0);
            }
            else
            {
                DrawLine(nodeView);
            }

            nodeView.AddToGrid(BinaryTreeGrid);
        }
        #endregion

        #region Node coordinates
        private void SetNodeCoordinates(BTSNodeView nodeView)
        {
            var node = nodeView.Node;

            Grid.SetRow(nodeView, 2 * (node.GetNodeDepth() - 1));

            if (node.GetNodeDepth() > BinaryTreeGrid.RowDefinitions.Count / 2)
            {
                RecalculateDelta(node.GetNodeDepth());

                ReplaceNodes(nodeView);
            }
            else
            {
                SetChildNodeCoordinates(nodeView);
            }
        }

        private void ReplaceNodes(BTSNodeView nodeView)
        {
            var node = nodeView.Node;

            RootColumn = (int)Math.Pow(2, node.GetNodeDepth() - 1);

            Grid.SetColumn(RootView, RootColumn);

            if (node.ParentNode.ParentNode != null)
            {
                var nodeList = GetNodeGridRow(2 * (node.GetNodeDepth() - 2));

                foreach (var nodePrevious in nodeList)
                {
                    SetNodeViewColumn((BTSNodeView)nodePrevious, node.GetNodeDepth());
                }
            }

            SetChildNodeCoordinates(nodeView);
        }

        private void SetNodeViewColumn(BTSNodeView nodeView, int maxDepth)
        {
            var node = nodeView.Node;

            if ((nodeView.Node).ParentNode == null)
                return;

            var parentList = GetNodeGridRow(2 * node.ParentNode.GetNodeDepth() - 2);

            foreach (var nodePrevious in parentList)
            {
                SetNodeViewColumn((BTSNodeView)nodePrevious, maxDepth);
            }

            SetChildNodeCoordinates(nodeView);
        }

        private void SetChildNodeCoordinates(BTSNodeView nodeView)
        {
            var node = nodeView.Node;

            if (node == nodeView.ParentNodeView.Node.RightNode)
            {
                Grid.SetColumn(nodeView, Grid.GetColumn(nodeView.ParentNodeView) + 1 + DepthDelta[node.GetNodeDepth()]);
            }
            else
            {
                Grid.SetColumn(nodeView, Grid.GetColumn(nodeView.ParentNodeView) - 1 - DepthDelta[node.GetNodeDepth()]);
            }
        }
        #endregion


        #region Tree clear
        public void ClearTree()
        {
            BinaryTreeGrid.Children.Clear();
        }
        #endregion

        #region Line drawing
        private void DrawLine(NodeView nodeView)
        {
            PairNodeLine line = new PairNodeLine();

            line.From = nodeView.ParentNode;
            line.To = nodeView;

            (BinaryTreeGrid.Parent as Grid).Children.Add(line);
        }

        #endregion

        #region Additional functions
        private void RecalculateDelta(int maxDepth)
        {
            DepthDelta.Add(maxDepth, 0);

            for (int depth = 2; depth < maxDepth; depth++)
            {
                DepthDelta[depth] = (int)Math.Pow(2, maxDepth - depth) - 1;
            }
        }

        private IEnumerable<UIElement> GetNodeGridRow(int depth) => BinaryTreeGrid.Children.Cast<UIElement>().Where(item => item is BTSNodeView && Grid.GetRow(item) == depth);

        private NodeView GetNodeView(BinaryTreeNode node) => (NodeView)BinaryTreeGrid.Children.Cast<UIElement>().FirstOrDefault(item => item is NodeView && (item as NodeView).TreeNode == node);
        #endregion
    }
}
