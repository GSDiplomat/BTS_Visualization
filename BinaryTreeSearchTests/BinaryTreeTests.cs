using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryTreeSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeSearch.Tests
{
    [TestClass()]
    public class BinaryTreeTests
    {
        Random rnd = new Random();
        string CorrectSequenceNodes;
        string SequenceNodes;

        List<int> Items = new List<int>();
        List<int> Sorted = new List<int>();

        [TestInitialize]
        public void Init()
        {
            CorrectSequenceNodes = string.Empty;
            SequenceNodes = string.Empty;

            for (int i = 0; i < 100; i++)
            {
                Items.Add(rnd.Next(0, 1000));
            }

            Sorted.Clear();
            Sorted.AddRange(Items.OrderBy(item => item).ToArray());

            foreach (int item in Sorted)
            {
                CorrectSequenceNodes += " " + item;
            }
        }

        [TestMethod()]
        public void TraversalTest()
        {
            // arrange
            var binaryTree = new BinaryTree();

            foreach (int item in Items)
            {
                binaryTree.AddNode(item);
            }

            // act
            SequenceNodes = binaryTree.Traversal(eTraversalType.Inorder);

            // assert
            Assert.AreEqual(CorrectSequenceNodes, SequenceNodes);
        }
    }
}