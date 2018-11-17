using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTreeLogic
{
    public class BinarySearchTree<T>: IEnumerable<T>
    {
        private IComparer<T> comparer;
        private Node<T> root;
        private int count;

        public int Count { get => count; }

        #region Constructor

        public BinarySearchTree()
        {
            if (!typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                throw new ArgumentException($"The {typeof(T)} must immplement IComparable<{typeof(T)}> interface.");
            }

            comparer = Comparer<T>.Default;
        }

        public BinarySearchTree(IEnumerable<T> collection) : this()
        {
            InitializeWithCollection(collection);
        }

        public BinarySearchTree(IComparer<T> comparer)
        {
            this.comparer = comparer = comparer ?? throw new ArgumentNullException($"The {nameof(comparer)} can not be null.");
        }

        public BinarySearchTree(IEnumerable<T> collection, IComparer<T> comparer) : this(comparer) 
        {
            InitializeWithCollection(collection);
        }

        #endregion Constructor

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

        public bool Contains(T item)
        {
           return ContainsCore(root, item);
        }

        #region Travels
        public IEnumerable<T> TravelInorder()
        {
            CheckNullRoot();

            return TravelInorder(root);

            IEnumerable<T> TravelInorder(Node<T> node)
            {
                if (node.leftChild != null)
                {
                    foreach (var item in TravelInorder(node.leftChild))
                    {
                        yield return item;
                    }                        
                }

                yield return node.Value;

                if (node.rightChild != null)
                {
                    foreach (var item in TravelInorder(node.rightChild))
                    {
                        yield return item;
                    }                        
                }
            }
        }

        public IEnumerable<T> TravelPreorder()
        {
            CheckNullRoot();

            return TravelPreorder(root);

            IEnumerable<T> TravelPreorder(Node<T> node)
            {
                yield return node.Value;

                if (node.leftChild != null)
                {
                    foreach (var item in TravelPreorder(node.leftChild))
                    {
                        yield return item;
                    }
                }                

                if (node.rightChild != null)
                {
                    foreach (var item in TravelPreorder(node.rightChild))
                    {
                        yield return item;
                    }
                }
            }
        }

        public IEnumerable<T> TravelPostorder()
        {
            CheckNullRoot();

            return TravelPostorder(root);

            IEnumerable<T> TravelPostorder(Node<T> node)
            {            
                if (node.leftChild != null)
                {
                    foreach (var item in TravelPostorder(node.leftChild))
                    {
                        yield return item;
                    }
                }

                if (node.rightChild != null)
                {
                    foreach (var item in TravelPostorder(node.rightChild))
                    {
                        yield return item;
                    }
                }

                yield return node.Value;
            }
        }
        #endregion Travels

        public IEnumerator<T> GetEnumerator() => TravelInorder().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private Node<T> AddCore(Node<T> node, T item)
        {            
            if (ReferenceEquals(node, null))
            {
                return new Node<T>(item);
            }

            int compareResult = comparer.Compare(node.Value, item);

            if (compareResult > 0)
            {
                node.leftChild = AddCore(node.leftChild, item);
            }
            else if (compareResult < 0)
            {
                node.rightChild = AddCore(node.rightChild, item);
            }
            return node;
        }

        private bool ContainsCore(Node<T> node, T item)
        {
            Node<T> current = node;
            while (comparer.Compare(current.Value, item) != 0)
            {
                if(ReferenceEquals(current, null))
                {
                    return false;
                }
                if (comparer.Compare(current.Value, item) > 0)
                {
                    current = current.leftChild;
                }
                else
                {
                    current = current.rightChild;
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
