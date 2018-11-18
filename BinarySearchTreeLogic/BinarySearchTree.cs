﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace BinarySearchTreeLogic
{
    /// <summary>
    /// Class for work with binary tree.
    /// </summary>
    /// <typeparam name="T">
    /// Any type.
    /// </typeparam>
    public class BinarySearchTree<T> : IEnumerable<T>
    {
        private IComparer<T> comparer;
        private Node<T> root;
        private int count;

        #region Constructor
        /// <summary>
        /// Constructor of binary tree.
        /// Return instance of tree with the default comparer.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when <typeparam name="T"> does not implement IComparable<<typeparam name="T">> interface.
        /// </exception>
        public BinarySearchTree()
        {
            if (!typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                throw new ArgumentException($"The {typeof(T)} must immplement IComparable<{typeof(T)}> interface.");
            }

            comparer = Comparer<T>.Default;
        }

        /// <summary>
        /// Constructor of binary tree.
        /// Return instance of tree with the default comparer.
        /// </summary>
        /// <param name="collection">
        /// Collection to initialize tree elements.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="collection"/> is null.
        /// </exception>
        public BinarySearchTree(IEnumerable<T> collection) : this()
        {
            InitializeWithCollection(collection);
        }

        /// <summary>
        /// Constructor of binary tree.
        /// Return instance of tree with the given comparer.
        /// </summary>
        /// <param name="comparer">
        /// Type that implement IComparer<T>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="comparer"/> is null.
        /// </exception>
        public BinarySearchTree(IComparer<T> comparer)
        {
            this.comparer = comparer = comparer ?? throw new ArgumentNullException($"The {nameof(comparer)} can not be null.");
        }

        /// <summary>
        /// Constructor of binary tree.
        /// </summary>
        /// <param name="collection">
        /// Collection to initialize tree elements.
        /// </param>
        /// <param name="comparer">
        /// Type that implement IComparer<T>.
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="comparer"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="collection"/> is null.
        /// </exception>
        public BinarySearchTree(IEnumerable<T> collection, IComparer<T> comparer) : this(comparer) 
        {
            InitializeWithCollection(collection);
        }

        #endregion Constructor

        /// <summary>
        /// Return count elements in tree.
        /// </summary>
        public int Count { get => count; }

        /// <summary>
        /// Add element in tree.
        /// </summary>
        /// <param name="item">
        /// Item for the inserting into tree.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="item"/> is null and type of <typeparam name="T"> is value type.
        /// </exception>
        public void Add(T item)
        {
            if (!typeof(T).IsValueType)
            {
                if (ReferenceEquals(item, null))
                {
                    throw new ArgumentNullException($"The {nameof(item)} can not be null.");
                }                   
            }
            
            root = AddCore(root, item);
            count++;
        }

        /// <summary>
        /// Checks for an item in the tree according to Equality protocol.
        /// </summary>
        /// <param name="item">
        /// The item for the checking.
        /// </param>
        /// <returns>
        /// True - if item exists in tree, otherwise  - false.
        /// </returns>
        public bool Contains(T item)
        {
           return ContainsCore(root, item);
        }

        #region Travels

        /// <summary>
        /// Performs a walk through the tree in inorder type. 
        /// </summary>
        /// <returns>
        /// Items collection of tree.
        /// </returns>
        public IEnumerable<T> TravelInorder()
        {
            CheckNullRoot();

            return TravelInorder(root);

            IEnumerable<T> TravelInorder(Node<T> node)
            {
                if (node.LeftChild != null)
                {
                    foreach (var item in TravelInorder(node.LeftChild))
                    {
                        yield return item;
                    }
                }
                
                yield return node.Value;

                if (node.RightChild != null)
                {
                    foreach (var item in TravelInorder(node.RightChild))
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        /// Performs a walk through the tree in preorder type. 
        /// </summary>
        /// <returns>
        /// Items collection of tree.
        /// </returns>
        public IEnumerable<T> TravelPreorder()
        {
            CheckNullRoot();

            return TravelPreorder(root);

            IEnumerable<T> TravelPreorder(Node<T> node)
            {
                yield return node.Value;

                if (node.LeftChild != null)
                {
                    foreach (var item in TravelPreorder(node.LeftChild))
                    {
                        yield return item;
                    }
                }                

                if (node.RightChild != null)
                {
                    foreach (var item in TravelPreorder(node.RightChild))
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        /// Performs a walk through the tree in postorder type. 
        /// </summary>
        /// <returns>
        /// Items collection of tree.
        /// </returns>
        public IEnumerable<T> TravelPostorder()
        {
            CheckNullRoot();

            return TravelPostorder(root);

            IEnumerable<T> TravelPostorder(Node<T> node)
            {            
                if (node.LeftChild != null)
                {
                    foreach (var item in TravelPostorder(node.LeftChild))
                    {
                        yield return item;
                    }
                }

                if (node.RightChild != null)
                {
                    foreach (var item in TravelPostorder(node.RightChild))
                    {
                        yield return item;
                    }
                }

                yield return node.Value;
            }
        }
        #endregion Travels

        /// <summary>
        /// Returns an iterator.
        /// </summary>
        /// <returns>
        /// Iterator of binary tree.
        /// </returns>
        public IEnumerator<T> GetEnumerator() => TravelInorder().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


        // TODO ASK
        // private IEnumerable<T> GenericTravel(Node<T> node, Func<Node<T>, Node<T>> child, Func<Node<T>, IEnumerable<T>> travelMethod)
        // {
        //    if (child(node) != null)
        //    {
        //        foreach (var item in travelMethod(child(node)))
        //        {
        //            yield return item;
        //        }
        //    }
        // }

        private Node<T> AddCore(Node<T> node, T item)
        {            
            if (ReferenceEquals(node, null))
            {
                return new Node<T>(item);
            }

            int compareResult = comparer.Compare(node.Value, item);

            if (compareResult > 0)
            {
                node.LeftChild = AddCore(node.LeftChild, item);
            }
            else if (compareResult < 0)
            {
                node.RightChild = AddCore(node.RightChild, item);
            }

            return node;
        }

        private bool ContainsCore(Node<T> node, T item)
        {
            Node<T> current = node;
            while (comparer.Compare(current.Value, item) != 0)
            {
                if (ReferenceEquals(current, null))
                {
                    return false;
                }

                if (comparer.Compare(current.Value, item) > 0)
                {
                    current = current.LeftChild;
                }
                else
                {
                    current = current.RightChild;
                }
            }

            return EqualityComparer<T>.Default.Equals(current.Value, item);
        }

        private void InitializeWithCollection(IEnumerable<T> collection)
        {
            collection = collection ?? throw new ArgumentNullException($"The {nameof(collection)} can not be null.");

            foreach (var item in collection)
            {
                Add(item);
            }
        }

        private void CheckNullRoot()
        {
            if (ReferenceEquals(root, null))
            {
                throw new ArgumentNullException($"The {nameof(root)} is null. Tree is empty.");
            }
        }
    }
}
