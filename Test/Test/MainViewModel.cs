using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Test
{
    public class MainViewModel:ViewModelBase
    {
        private Author NewAuthor;
        private string bookName;
        private string firstName;
        private string secondName;
        private string fatherName;

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string SecondName
        {
            get
            {
                return secondName;
            }
            set
            {
                secondName = value;
                RaisePropertyChanged("SecondName");
            }
        }

        public string FatherName
        {
            get
            {
                return fatherName;
            }
            set
            {
                fatherName = value;
                RaisePropertyChanged("FatherName");
            }
        }
        public string BookName
        {
            get
            {
                return bookName;
            }
            set
            {
                bookName = value;
                RaisePropertyChanged("BookName");
            }
        }
        public ObservableCollection<string> BookOfAuthor { get; set; }
        public ObservableCollection<Author> AllAuthors { get; set; }

        public ICommand AddAuthor { set; get; }
        public ICommand AddBook { set; get; }
        public ICommand OpenWindowAuthor { set; get; }
        public ICommand AddBookToAuthor { get; set; }
        public ICommand DeleteBookFromAuthor { get; set; }
        public ICommand EditAuthor { get; set; }
        public ICommand ShowAllAuthors { get; set; }
        public ICommand DeleteAuthor { get; set; }

        public MainViewModel()
        {
            NewAuthor = new Author();

            BookOfAuthor = new ObservableCollection<string>();
            AllAuthors = new ObservableCollection<Author>();

            AddAuthor = new RelayCommand(AddAuthorMethod);
            AddBook = new RelayCommand(AddBookMethod);
            OpenWindowAuthor = new RelayCommand(OpenWindowAuthorMethod);
            AddBookToAuthor = new RelayCommand(AddBookToAuthorMethod);
            DeleteBookFromAuthor = new RelayCommand(DeleteBookFromAuthorMethod);
            EditAuthor = new RelayCommand(EditAuthorMethod);
            ShowAllAuthors = new RelayCommand(ShowAllAuthorsMethod);
            DeleteAuthor = new RelayCommand(DeleteAuthorMethod);
        }

        public void AddAuthorMethod()
        {
            Model1 context = new Model1();
            NewAuthor.FatherName = FatherName;
            NewAuthor.FirstName = FirstName;
            NewAuthor.SecondName = SecondName;
            context.Authors.Add(NewAuthor);
            context.SaveChanges();
        }

        public void AddBookMethod()
        {
            Model1 context = new Model1();
            Book NewBook = new Book();
            NewBook.BookName = BookName;
            context.Books.Add(NewBook);
            context.SaveChanges();
        }

        public void OpenWindowAuthorMethod()
        {
            Model1 context = new Model1();
            var OneAuthor = context.Authors.Where(c => (c.SecondName == SecondName) & (c.FatherName == FatherName) & (c.FirstName == FirstName));
            if (OneAuthor.Count()!=0)
            {
                ObservableCollection<Book> Books = context.Authors.Where(c => (c.SecondName == SecondName) & (c.FatherName == FatherName) & (c.FirstName == FirstName)).Select(c=>c.Books).FirstOrDefault();
                foreach(Book b in Books)
                {
                    BookOfAuthor.Add(b.BookName);
                }
                NewAuthor.FatherName = FatherName;
                NewAuthor.FirstName = FirstName;
                NewAuthor.SecondName = SecondName;
                WindowAuthors WindowNew = new WindowAuthors();
                WindowNew.Show();
            }
            else MessageBox.Show("Автор не найден");
        }

        public void AddBookToAuthorMethod()
        {
            BookOfAuthor.Add(BookName);

            Model1 context = new Model1();
            var OneBook = context.Books.Where(c => (c.BookName == BookName));
            if (OneBook.Count() == 0) AddBookMethod();
            Add();
        }

        public void DeleteBookFromAuthorMethod()
        {
            Boolean proverca=false;
            foreach (string b in BookOfAuthor)
            {
                if (b == BookName) proverca = true;
            }
            if (proverca == true)
            {
                string name = BookName;
                BookOfAuthor.Remove(name);

                Delete(name);
            }
            else MessageBox.Show("Книга в списке отсутствует");
        }

        public void EditAuthorMethod()
        {
            Model1 context = new Model1();
            Author OneAuthor = context.Authors.Where(c => (c.SecondName == NewAuthor.SecondName) & (c.FatherName == NewAuthor.FatherName) & (c.FirstName == NewAuthor.FirstName)).FirstOrDefault();
            OneAuthor.SecondName = SecondName;
            OneAuthor.FirstName = FirstName;
            OneAuthor.FatherName = FatherName;
            context.SaveChanges();
        }

        public void ShowAllAuthorsMethod()
        {
            Model1 context = new Model1();
            foreach (Author a in context.Authors)
            {
                AllAuthors.Add(a);
            }
        }

        public void DeleteAuthorMethod()
        {
            Model1 context = new Model1();
            Author OneAuthor = context.Authors.Where(c => (c.SecondName == SecondName) & (c.FatherName == FatherName) & (c.FirstName == FirstName))
                .Include(c => c.Books).FirstOrDefault();
            context.Authors.Remove(OneAuthor);
            context.SaveChanges();
        }

        public void Add()
        {
            Model1 context = new Model1();
            Book _Book = context.Books.Where(c => (c.BookName == BookName)).FirstOrDefault();
            Author OneAuthor = context.Authors.Where(c => (c.SecondName == NewAuthor.SecondName) & (c.FatherName == NewAuthor.FatherName) & (c.FirstName == NewAuthor.FirstName)).FirstOrDefault();
            OneAuthor.Books.Add(_Book);
            context.SaveChanges();
        }

        public void Delete(string n)
        {
            Model1 context = new Model1();
            Author OneAuthor = context.Authors.Where(c => (c.SecondName == NewAuthor.SecondName) & (c.FatherName == NewAuthor.FatherName) & (c.FirstName == NewAuthor.FirstName))
                .Include(c => c.Books).FirstOrDefault();
            Book book = OneAuthor.Books.First(c => c.BookName == n);
            OneAuthor.Books.Remove(book);
            context.SaveChanges();
        }
        
    }
}
