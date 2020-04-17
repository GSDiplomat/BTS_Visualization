using BinaryTreeSearch;
using BTSVisualization.BinaryTreeControl;
using System;
using System.Collections.Generic;
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

        public int BinaryTreeData
        {
            get { return (int)GetValue(BinaryTreeProperty); }
            set { SetValue(BinaryTreeProperty, value); }
        }

        public static readonly DependencyProperty BinaryTreeProperty =
            DependencyProperty.Register("BinaryTreeData", typeof(int), typeof(BinaryTreeView), new PropertyMetadata(0, BinaryTreeChanged));

        private static void BinaryTreeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ChangeGrid((sender as BinaryTreeView).TreeGrid, (int)e.OldValue, (int)e.NewValue);
        }

        private static void ChangeGrid(Grid grid, int oldValue, int newValue)
        {
            if (oldValue < newValue)
            {
                for (int i = 0; i < 2; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                }

                for (int i = 1; i < Math.Pow(2, newValue); i += 2)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition());

                    BTSNodeView node = new BTSNodeView();
                    Grid.SetRow(node, 2 * (newValue - 1));
                    Grid.SetColumn(node, i);
                    grid.Children.Add(node);
                }

                ReplaceNodes(grid, newValue);
            }
        }

        private static void ReplaceNodes(Grid grid, int depth)
        {
            List<UIElement> rowNodes;

            for (int i = 0; i < 2 * depth; i += 2)
            {
                rowNodes = grid.Children.Cast<UIElement>().Where(item => Grid.GetRow(item) == i).ToList();

                int colCounter = 0;

                foreach (var node in rowNodes)
                {
                    int row = ((Grid.GetRow(node) + 1) + 2) / 2;
                    int index = rowNodes.IndexOf(node);
                    int nodeNewRow = (int)(Math.Pow(2, depth - row) * (1 + 2 * index));

                    colCounter += nodeNewRow;

                    Grid.SetColumn(node, nodeNewRow);
                }
            }
        }
    }
}
