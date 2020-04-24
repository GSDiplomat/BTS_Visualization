using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTreeSearch.Tests
{
    [TestFixture()]
    public class BinaryTreeTests
    {
        Random rnd = new Random();
        string CorrectSequenceNodes;
        string SequenceNodes;

        List<int> Items = new List<int> { 9, 6, 17, 3, 8, 16, 20, 1, 4, 7, 12, 19, 21, 2, 5, 11, 14, 18, 10, 13, 15 };
        List<int> Sorted = new List<int>();

        BinaryTree BinaryTree;

        [SetUp]
        public void Setup()
        {
            BinaryTree = new BinaryTree();

            foreach (var item in Items)
            {
                BinaryTree.AddNode(item);
            }
        }

        [Test]
        public void TraversalInorder_DefaultBTS_AscendingSequence()
        {
            // arrange
            CorrectSequenceNodes = string.Empty;
            SequenceNodes = string.Empty;

            Sorted.Clear();
            Sorted.AddRange(Items.OrderBy(item => item).ToArray());

            foreach (var item in Sorted)
            {
                CorrectSequenceNodes += " " + item;
            }

            // act
            SequenceNodes = BinaryTree.Traversal(eTraversalType.Inorder);

            // assert
            Assert.AreEqual(CorrectSequenceNodes, SequenceNodes);
        }

        [Test]
        public void GetMaxDepth_DefaultBTS_6Returned()
        {
            // arrange
            int expectDepth = 6;

            // act
            int maxDepth = BinaryTree.GetMaxDepth();

            // assert
            Assert.AreEqual(expectDepth, maxDepth);
        }

        [Test]
        public void GetNodeDepth_BTNode14_5Returned()
        {
            // arrange
            int expectDepth = 5;

            // act
            int maxDepth = BinaryTree.FindNode(14).GetNodeDepth();

            // assert
            Assert.AreEqual(expectDepth, maxDepth);
        }

        [Test]
        public void GetNodeDepth_BTNode9_1Returned()
        {
            // arrange
            int expectDepth = 1;

            // act
            int maxDepth = BinaryTree.FindNode(9).GetNodeDepth();

            // assert
            Assert.AreEqual(expectDepth, maxDepth);
        }

        [Test]
        public void GetNodeDepth_BTNode3_3Returned()
        {
            // arrange
            int expectDepth = 3;

            // act
            int maxDepth = BinaryTree.FindNode(3).GetNodeDepth();

            // assert
            Assert.AreEqual(expectDepth, maxDepth);
        }

        [Test]
        public void GetNodeDepth_BTNode21_4Returned()
        {
            // arrange
            int expectDepth = 4;

            // act
            int maxDepth = BinaryTree.FindNode(21).GetNodeDepth();

            // assert
            Assert.AreEqual(expectDepth, maxDepth);
        }

        [Test]
        public void GetNodeDepth_BTNode10_6Returned()
        {
            // arrange
            int expectDepth = 6;

            // act
            int maxDepth = BinaryTree.FindNode(10).GetNodeDepth();

            // assert
            Assert.AreEqual(expectDepth, maxDepth);
        }
    }
}