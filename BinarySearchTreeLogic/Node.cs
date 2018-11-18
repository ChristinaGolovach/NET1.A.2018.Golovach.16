namespace BinarySearchTreeLogic
{
    /// <summary>
    /// Class for work with node of binary tree.
    /// </summary>
    /// <typeparam name="T">
    /// Any type.
    /// </typeparam>
    internal class Node<T>
    {
        private T value;       

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">
        /// Value for the node.
        /// </param>
        public Node(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Return left child of node.
        /// </summary>
        public Node<T> LeftChild { get; set; }

        /// <summary>
        /// Return right child of node.
        /// </summary>
        public Node<T> RightChild { get; set; }

        /// <summary>
        /// Return value of node.
        /// </summary>
        public T Value { get => value; }
    }
}
