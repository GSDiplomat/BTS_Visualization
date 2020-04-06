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
        public BinaryTree BinaryTree
        {
            get { return (BinaryTree)GetValue(BinaryTreeProperty); }
            set { SetValue(BinaryTreeProperty, value); }
        }

        public static readonly DependencyProperty BinaryTreeProperty =
            DependencyProperty.Register("Data", typeof(BinaryTree), typeof(BinaryTreeView), new PropertyMetadata(new BinaryTree(), BinaryTreeChanged));

        static int num = 0;

        private static void BinaryTreeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            BinaryTreeView binaryTreeView = new BinaryTreeView();
            binaryTreeView.TreeGrid.RowDefinitions.Add(new RowDefinition());

            Button btn = new Button();
            btn.Content = "test";
            Grid.SetRow(btn, num);

            binaryTreeView.TreeGrid.Children.Add(btn);

            num++;
        }
    }
}
