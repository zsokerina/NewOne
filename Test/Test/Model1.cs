namespace Test
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;

    public class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }

    public class Author:ObservableObject
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FatherName { get; set; }

        public ObservableCollection<Book> Books { get; set; }
        public Author()
        {
            Books = new ObservableCollection<Book>();
        }
    }

    public class Book:ObservableObject
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public ObservableCollection<Author>  Authors{ get; set; }
        public Book()
        {
            Authors = new ObservableCollection<Author>();
        }
    }
}