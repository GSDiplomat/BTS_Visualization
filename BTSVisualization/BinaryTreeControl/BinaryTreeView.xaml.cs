using BinaryTreeSearch;
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

                    Button btn = new Button();
                    btn.Content = "test";
                    Grid.SetRow(btn, newValue - 1);
                    Grid.SetColumn(btn, i);
                    grid.Children.Add(btn);
                }
            }
        }

        private static void ReplaceNodes(Grid grid, int depth)
        {
        }

        public static DataGridCell GetCell(Grid dataGrid, int row, int column)
        {
            RowDefinition rowContainer = GetRow(dataGrid, row);
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                // try to get the cell but it may possibly be virtualized
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    // now try to bring into view and retreive the cell
                    dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);

                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }

                return cell;
            }

            return null;
        }
    }
}
