using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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

            foreach (var item in Sorted)
            {
                CorrectSequenceNodes += " " + item;
            }
        }

        [TestMethod()]
        public void TraversalTest()
        {
            // arrange
            var binaryTree = new BinaryTree();

            foreach (var item in Items)
            {
                binaryTree.AddNode(item);
            }

            // act
            SequenceNodes = binaryTree.Traversal(eTraversalType.Inorder);

            // assert
            Assert.AreEqual(CorrectSequenceNodes, SequenceNodes);
        }

        [TestMethod()]
        public void RemovedNodeTest()
        {
            // arrange
            var binaryTree = new BinaryTree();

            foreach (var item in Items)
            {
                binaryTree.AddNode(item);
            }

            //act
            var removingNode = binaryTree.FindNode(Items[rnd.Next(0, Items.Count - 1)]);

            binaryTree.RemoveNode(removingNode);

            SequenceNodes = binaryTree.Traversal(eTraversalType.Inorder);

            //form correct sequence
            Sorted.Remove(removingNode.NodeValue);

            CorrectSequenceNodes = string.Empty;

            foreach (var item in Sorted)
            {
                CorrectSequenceNodes += " " + item;
            }

            // assert
            Assert.AreEqual(CorrectSequenceNodes, SequenceNodes);
        }
    }
}