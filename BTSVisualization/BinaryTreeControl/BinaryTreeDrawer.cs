using BinaryTreeSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BTSVisualization
{
    public static class BinaryTreeDrawer
    {
        public static Grid BinaryTreeGrid;
        public static int RootColumn;

        private static Dictionary<int, int> DepthDelta = new Dictionary<int, int>();

        #region MaxDepthChanged
        public static void ChangeGrid(int oldValue, int newValue)
        {
            if (oldValue < newValue)
            {
                for (int i = 0; i < 2; i++)
                {
                    BinaryTreeGrid.RowDefinitions.Add(new RowDefinition());
                }

                for (int i = 1; i < Math.Pow(2, newValue); i += 2)
                {
                    BinaryTreeGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }
            }
        }
        #endregion

        #region NodeAdded
        public static void AddNode(BinaryTreeNode node)
        {
            BTSNodeView nodeView = new BTSNodeView(node);

            if (node.ParentNode == null)
            {
                Grid.SetRow(nodeView, 0);
                Grid.SetColumn(nodeView, 1);
                BinaryTreeGrid.Children.Add(nodeView);

                DepthDelta.Add(1, 0);
            }
            else
            {
                nodeView.SetNodeCoordinates();

                BinaryTreeGrid.Children.Add(nodeView);
            }
        }
        #endregion

        #region Node coordinates
        private static void SetNodeCoordinates(this BTSNodeView nodeView)
        {
            var node = (BinaryTreeNode)nodeView;
            var parentNodeView = BinaryTreeGrid.Children.Cast<BTSNodeView>().FirstOrDefault(item => (BinaryTreeNode)item == node.ParentNode);

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

        private static void ReplaceNodes(BTSNodeView nodeView)
        {
            var node = (BinaryTreeNode)nodeView;
            var rootView = BinaryTreeGrid.Children.Cast<BTSNodeView>().FirstOrDefault(item => Grid.GetRow(item) == 0);

            RootColumn = (int)Math.Pow(2, node.GetNodeDepth() - 1);

            Grid.SetColumn(rootView, RootColumn);

            if (node.ParentNode.ParentNode != null)
            {
                var nodeList = BinaryTreeGrid.Children.Cast<BTSNodeView>().Where(item => Grid.GetRow(item) == 2 * (node.GetNodeDepth() - 2));

                foreach (var nodePrevious in nodeList)
                {
                    SetNodeViewColumn(nodePrevious, node.GetNodeDepth());
                }
            }

            SetChildNodeCoordinates(nodeView);
        }

        private static void SetNodeViewColumn(BTSNodeView nodeView, int maxDepth)
        {
            var node = (BinaryTreeNode)nodeView;

            if (((BinaryTreeNode)nodeView).ParentNode == null)
                return;

            var parentList = BinaryTreeGrid.Children.Cast<BTSNodeView>().Where(item => Grid.GetRow(item) == 2 * node.ParentNode.GetNodeDepth() - 2);

            foreach (var nodePrevious in parentList)
            {
                SetNodeViewColumn(nodePrevious, maxDepth);
            }

            SetChildNodeCoordinates(nodeView);
        }

        private static void SetChildNodeCoordinates(BTSNodeView nodeView)
        {
            var node = (BinaryTreeNode)nodeView;
            var parentNodeView = BinaryTreeGrid.Children.Cast<BTSNodeView>().FirstOrDefault(item => (BinaryTreeNode)item == node.ParentNode);

            if (node == ((BinaryTreeNode)parentNodeView).RightNode)
            {
                Grid.SetColumn(nodeView, Grid.GetColumn(parentNodeView) + 1 + DepthDelta[node.GetNodeDepth()]);
            }
            else
            {
                Grid.SetColumn(nodeView, Grid.GetColumn(parentNodeView) - 1 - DepthDelta[node.GetNodeDepth()]);
            }
        }
        #endregion

        private static void RecalculateDelta(int maxDepth)
        {
            DepthDelta.Add(maxDepth, 0);

            for (int depth = 2; depth < maxDepth; depth++)
            {
                DepthDelta[depth] = (int)Math.Pow(2, maxDepth - depth) - 1;
            }
        }

        #region Очистка дерева
        public static void ClearTree()
        {
            BinaryTreeGrid = new Grid();
        }
        #endregion
    }
}
