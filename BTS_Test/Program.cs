using BinaryTreeSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> Items = new List<int>();
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                Items.Add(rnd.Next(0, 1000));
            }

            var binaryTree = new BinaryTree();

            foreach (int item in Items)
            {
                binaryTree.AddNode(item);
            }

            // act
            string SequenceNodes = binaryTree.Traversal(eTraversalType.Inorder);
        }
    }
}
