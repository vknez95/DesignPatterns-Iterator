using System;
using System.Linq;
using System.Xml.Serialization;
using DemoTree;
using Xunit;

namespace UnitTest
{
    public class DemoTreeShould
    {
        [Fact]
        public void StartEmpty()
        {
            var myTree = new DemoTree<string>();

            Assert.Null(myTree.Value);
        }

        [Fact]
        public void StoreValueInRootNode()
        {
            int expectedValue = 1;
            DemoTree<int> myTree = CreateTreeWithValues(expectedValue);

            Assert.Equal(expectedValue, myTree.Value);
        }

        [Fact]
        public void StoreSecondValueInLeftNode()
        {
            int expectedValue = 2;
            DemoTree<int> myTree = CreateTreeWithValues(expectedValue);

            Assert.Equal(expectedValue, myTree.LeftChild.Value);
        }

        [Fact]
        public void StoreThirdValueInRightNode()
        {
            int expectedValue = 3;
            DemoTree<int> myTree = CreateTreeWithValues(expectedValue);

            Assert.Equal(expectedValue, myTree.RightChild.Value);
        }

        [Fact]
        public void StoreFourthValueInLeftLeftNode()
        {
            int expectedValue = 4;
            DemoTree<int> myTree = CreateTreeWithValues(expectedValue);

            Assert.Equal(expectedValue, myTree.LeftChild.LeftChild.Value);
        }

        [Fact]
        public void StoreFifthValueInLeftRightNode()
        {
            int expectedValue = 5;
            DemoTree<int> myTree = CreateTreeWithValues(expectedValue);

            Assert.Equal(expectedValue, myTree.LeftChild.RightChild.Value);
        }

        [Fact]
        public void StoreSixthValueInRightLeftNode()
        {
            int expectedValue = 6;
            DemoTree<int> myTree = CreateTreeWithValues(expectedValue);

            DisplayTree(myTree);
            Assert.Equal(expectedValue, myTree.RightChild.LeftChild.Value);
        }

        [Fact]
        public void ListValuesInDepthFirstOrder()
        {
            int expectedValue = 6;
            DemoTree<int> myTree = CreateTreeWithValues(expectedValue);

            DisplayTree(myTree);
            var valuesString = String.Join(" ", myTree.ToList().ToArray());

            Assert.Equal("1 2 4 5 3 6", valuesString);
        }

        [Fact]
        public void ListValuesInDepthFirstOrderWithEnumerator()
        {
            int expectedValue = 6;
            DemoTree<int> myTree = CreateTreeWithValues(expectedValue);

            DisplayTree(myTree);
            var valuesString = String.Join(" ", myTree.ToArray());

            Assert.Equal("1 2 4 5 3 6", valuesString);
        }

        [Fact]
        public void ListValuesInBreadthFirstOrderWithEnumerator()
        {
            int expectedValue = 6;
            DemoTree<int> myTree = CreateTreeWithValues(expectedValue);
            myTree.UseBreadthFirstEnumerator = true;

            DisplayTree(myTree);
            var valuesString = String.Join(" ", myTree.ToArray());

            Assert.Equal("1 2 3 4 5 6", valuesString);
        }

        [Fact]
        public void InitialTreeHasDepthZero()
        {
            var myTree = new DemoTree<int>();
            myTree.Add(1);

            Assert.Equal(0, myTree.Depth());
        }

        private DemoTree<int> CreateTreeWithValues(int numberOfValues)
        {
            if (numberOfValues <= 0) return null;

            var tree = new DemoTree<int>(1);
            for (int i = 2; i <= numberOfValues; i++)
            {
                tree.Add(i);
            }
            return tree;
        }

        private void DisplayTree(DemoTree<int> tree)
        {
            var serializer = new XmlSerializer(tree.GetType());
            serializer.Serialize(Console.Out, tree);
        }
    }
}
