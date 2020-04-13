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

        List<int> Items = new List<int>();
        List<int> Sorted = new List<int>();

        [SetUp]
        public void Setup()
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

        [Test]
        public void TraversalInorderTest()
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

        [Test]
        public void GetMaxDepthTest()
        {
            // arrange
            var binaryTree = new BinaryTree();

            binaryTree.AddNode(100);
            binaryTree.AddNode(150);
            binaryTree.AddNode(200);
            binaryTree.AddNode(170);
            binaryTree.AddNode(210);
            binaryTree.AddNode(70);
            binaryTree.AddNode(50);
            binaryTree.AddNode(90);
            binaryTree.AddNode(10);
            binaryTree.AddNode(55);
            binaryTree.AddNode(60);
            binaryTree.AddNode(58);
            binaryTree.AddNode(65);

            int expectDepth = 6;
            // act
            int maxDepth = binaryTree.GetMaxDepth();

            // assert
            Assert.AreEqual(expectDepth, maxDepth);
        }
    }
}