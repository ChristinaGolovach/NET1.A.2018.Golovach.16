using System.Collections.Generic;
using System.Linq;
using BinarySearchTreeLogic.Tests.Classes;
using NUnit.Framework;

namespace BinarySearchTreeLogic.Tests
{
    [TestFixture]
    public class BinarySearchTreeTests
    {
        #region Create tree
        [TestCase(new int[] { 8, 3, 1, 6, 10, 14, 7, 13 }, 8)]
        public void Constructor_CreateBinnaryTreeWithBCLTypeInt_InstanceIsNotNuul(int[] collection, int expectedResult)
        {
            // Act
            BinarySearchTree<int> binarySearchTree = new BinarySearchTree<int>(collection);

            // Assert           
            Assert.AreEqual(binarySearchTree.Count, expectedResult);
        }

        [TestCase(new string[] { "A", "B", "D", "E", "C", "F", "G" }, 7)]
        public void Constructor_CreateBinnaryTreeWithBCLTypeString_InstanceIsNotNuul(string[] collection, int expectedResult)
        {
            // Act
            BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>(collection);

            // Assert           
            Assert.AreEqual(binarySearchTree.Count, expectedResult);
        }

        public void Constructor_CreateBinnaryTreeWithBook_InstanceIsNotNuul()
        {
            // Act
            BinarySearchTree<Book> binarySearchTree = new BinarySearchTree<Book>(CreateBookArrayWIthCountTree());

            // Assert           
            Assert.AreEqual(binarySearchTree.Count, 3);
        }

        public void Constructor_CreateBinnaryTreeWithPoint_InstanceIsNotNuul()
        {
            // Act
            BinarySearchTree<Point> binarySearchTree = new BinarySearchTree<Point>(CreatePointArrayWithCountTree());

            // Assert           
            Assert.AreEqual(binarySearchTree.Count, 3);
        }

        #endregion Create tree

        #region Contains tests

        [TestCase(12)]
        [TestCase(-10)]
        [TestCase(13)]
        public void Contains_AddIntItem_ReturnTrueContains(int item)
        {
            // Arrange
            Comparer<int> comparer =  Comparer<int>.Create((x, y) =>  x.CompareTo(y));
            BinarySearchTree<int> binarySearchTree = new BinarySearchTree<int>(new int[] {9, 8, 3}, comparer);

            // Act
            binarySearchTree.Add(item);

            // Assert
            Assert.IsTrue(binarySearchTree.Contains(item));
        }

        [TestCase("C")]
        [TestCase("Q")]
        public void Contains_AddStringItem_ReturnTrueContains(string item)
        {
            // Arrange
            Comparer<string> comparer = Comparer<string>.Create((x, y) => x.CompareTo(y));
            BinarySearchTree<string> binarySearchTree = new BinarySearchTree<string>(new string[] { "A", "B", "D" }, comparer);

            // Act
            binarySearchTree.Add(item);

            // Assert
            Assert.IsTrue(binarySearchTree.Contains(item));
        }

        [TestCase(2, "Two")]
        [TestCase(5, "Five")]
        public void Contains_AddBookItem_ReturnTrueContainsWhenBookInstanceTheSame(int price, string name)
        {
            // Arrange
            Comparer<Book> comparer = Comparer<Book>.Create((first, second) => first.Name.CompareTo(second.Name) - first.Price.CompareTo(second.Price));
            BinarySearchTree<Book> binarySearchTree = new BinarySearchTree<Book>(CreateBookArrayWIthCountTree(), comparer);
            Book book = new Book { Price = price, Name = name };

            // Act
            binarySearchTree.Add(book);

            // Assert
            Assert.IsTrue(binarySearchTree.Contains(book));
        }

        [TestCase(2, "Two")]
        [TestCase(5, "Five")]
        public void Contains_AddBookItem_ReturnFalseContainsWhenBookInstanceIsNew(int price, string name)
        {
            // Arrange
            Comparer<Book> comparer = Comparer<Book>.Create((first, second) => first.Name.CompareTo(second.Name) - first.Price.CompareTo(second.Price));
            BinarySearchTree<Book> binarySearchTree = new BinarySearchTree<Book>(CreateBookArrayWIthCountTree(), comparer);

            // Act
            binarySearchTree.Add(new Book { Price = price, Name = name });

            // Assert
            Assert.IsTrue(!binarySearchTree.Contains(new Book { Price = price, Name = name }));
        }
        
        [TestCase(2, 2)]
        [TestCase(4, 4)]
        public void Contains_AddPointItem_ReturnTrueContainsWhenTheSameInstance(int x, int y)
        {
            // Arrange
            Comparer<Point> comparer = Comparer<Point>.Create((first, second) => first.X.CompareTo(second.X) + first.Y.CompareTo(second.Y));
            BinarySearchTree<Point> binarySearchTree = new BinarySearchTree<Point>(CreatePointArrayWithCountTree(), comparer);
            Point point = new Point(x, y);

            // Act
            binarySearchTree.Add(point);

            // Assert
            Assert.IsTrue(binarySearchTree.Contains(point));
        }

        [TestCase(2, 2)]
        [TestCase(4, 4)]
        public void Contains_AddPointItem_ReturnTrueContainsWnenNewInstance(int x, int y)
        {
            // Arrange
            Comparer<Point> comparer = Comparer<Point>.Create((first, second) => first.X.CompareTo(second.X) + first.Y.CompareTo(second.Y));
            BinarySearchTree<Point> binarySearchTree = new BinarySearchTree<Point>(CreatePointArrayWithCountTree(), comparer);

            // Act
            binarySearchTree.Add(new Point(x, y));

            // Assert
            Assert.IsTrue(binarySearchTree.Contains(new Point(x, y)));
        }

        #endregion Contains tests

        #region Travels tests

        [TestCase(new int[] {8, 3, 10, 1, 6, 14, 7, 13}, new int[] {1, 3, 6, 7, 8, 10, 13, 14})]
        public void TravelInorder_PassIntArray(int[] array, int[] result)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(array);
            int[] inorderArray =  tree.TravelInorder().ToArray();

            CollectionAssert.AreEqual(result, inorderArray);
        }

        [TestCase(new int[] { 8, 3, 10, 1, 6, 14, 7, 13 }, new int[] {8, 3, 1, 6, 7, 10, 14, 13 })]
        public void TravelPreorder_PassIntArray(int[] array, int[] result)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(array);
            int[] inorderArray = tree.TravelPreorder().ToArray();

            CollectionAssert.AreEqual(result, inorderArray);
        }

        [TestCase(new int[] { 8, 3, 10, 1, 6, 14, 7, 13 }, new int[] { 1, 7, 6, 3, 13, 14, 10, 8 })]
        public void TravelPost_PassIntArray(int[] array, int[] result)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(array);
            int[] inorderArray = tree.TravelPostorder().ToArray();

            CollectionAssert.AreEqual(result, inorderArray);
        }


        #endregion Travels tests

        private static Book[] CreateBookArrayWIthCountTree()
        {
            return new Book[] { new Book(8, "Eight"), new Book(3, "Tree"), new Book(1, "One")};
        }

        private Point[] CreatePointArrayWithCountTree()
        {
            return new Point[] { new Point(8, 8), new Point (3, 3), new Point(1, 1)};
        } 
    }
}
