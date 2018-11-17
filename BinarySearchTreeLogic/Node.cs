namespace BinarySearchTreeLogic
{
    class Node<T>
    {
        private T value;

        public Node<T> leftChild { get; set; }
        public Node<T> rightChild { get; set; }
        public T Value { get => value; }

        public Node(T value)
        {
            this.value = value;
        }
    }
}
