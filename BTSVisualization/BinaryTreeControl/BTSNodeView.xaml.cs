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
    /// Логика взаимодействия для BTSNodeView.xaml
    /// </summary>
    public partial class BTSNodeView : UserControl
    {
        BinaryTreeNode Node;
        public BTSNodeView()
        {
            InitializeComponent();

            //Visibility = Visibility.Collapsed;
        }
        public BTSNodeView(BinaryTreeNode node) : this()
        {
            Node = node;

            DataContext = Node;
        }

        public static explicit operator BinaryTreeNode(BTSNodeView bTSNodeView) => bTSNodeView.Node;
    }
}
