using System;

namespace BinarySearchTreeLogic.Tests.Classes
{
    public class Book : IComparable<Book>
    {
        public int Price { get; set; }
        public string Name { get; set; }

        public Book() { }

        public Book(int price, string name)
        {
            Price = price;
            Name = name;
        }

        public int CompareTo(Book other)
        {
            return Price.CompareTo(other.Price);
        }
    }
}
