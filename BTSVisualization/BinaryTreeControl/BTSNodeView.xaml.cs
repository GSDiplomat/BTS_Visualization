using BinaryTreeSearch;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace BTSVisualization
{
    /// <summary>
    /// Логика взаимодействия для BTSNodeView.xaml
    /// </summary>
    public partial class BTSNodeView : UserControl
    {
        private BTSNodeView _parentNodeView;

        public PairNodeLine NodeViewLine;
        public bool IsSelected = false;
        public BTSNodeView()
        {
            InitializeComponent();
        }

        public BTSNodeView(BinaryTreeNode node) : this()
        {
            Node = node;

            DataContext = Node;
        }

        public BinaryTreeNode Node { get; }

        public BTSNodeView ParentNodeView => _parentNodeView ?? 
            (_parentNodeView = (BTSNodeView)(Parent as Grid).Children.Cast<UIElement>().FirstOrDefault(item => item is BTSNodeView && (item as BTSNodeView).Node == Node.ParentNode));

    }
}
