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
        private AuthorViewModel NewAuthor;
        private string NameOfBook;
        private string bookName;
        public AuthorViewModel selectedAuthor;

        public AuthorViewModel SelectedAuthor
        {
            get
            {
                return selectedAuthor;
            }
            set
            {
                selectedAuthor = value;
                RaisePropertyChanged("SelectedAuthor");
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
        public ObservableCollection<AuthorViewModel> AllAuthors { get; set; }
        public ObservableCollection<AuthorViewModel> AuthorsOfBook { get; set; }
        public ObservableCollection<string> AllBooks { get; set; }

        public ICommand AddAuthor { set; get; }
        public ICommand AddBook { set; get; }
        public ICommand OpenWindowAuthor { set; get; }
        public ICommand AddBookToAuthor { get; set; }
        public ICommand DeleteBookFromAuthor { get; set; }
        public ICommand EditAuthor { get; set; }
        public ICommand ShowAllAuthors { get; set; }
        public ICommand DeleteAuthor { get; set; }
        public ICommand ShowAllBooks { get; set; }
        public ICommand DeleteBook { get; set; }
        public ICommand OpenWindowBook { get; set; }
        public ICommand AddAuthorToBook { get; set; }
        public ICommand DeleteAuthorFromBook { get; set; }
        public ICommand EditBook { get; set; }

        public MainViewModel()
        {
            NewAuthor = new AuthorViewModel();
            SelectedAuthor = new AuthorViewModel();

            BookOfAuthor = new ObservableCollection<string>();
            AllAuthors = new ObservableCollection<AuthorViewModel>();
            AllBooks = new ObservableCollection<string>();
            AuthorsOfBook = new ObservableCollection<AuthorViewModel>();

            AddAuthor = new RelayCommand(AddAuthorMethod);
            AddBook = new RelayCommand(AddBookMethod);
            OpenWindowAuthor = new RelayCommand(OpenWindowAuthorMethod);
            AddBookToAuthor = new RelayCommand(AddBookToAuthorMethod);
            DeleteBookFromAuthor = new RelayCommand(DeleteBookFromAuthorMethod);
            EditAuthor = new RelayCommand(EditAuthorMethod);
            ShowAllAuthors = new RelayCommand(ShowAllAuthorsMethod);
            DeleteAuthor = new RelayCommand(DeleteAuthorMethod);
            ShowAllBooks = new RelayCommand(ShowAllBooksMethod);
            DeleteBook = new RelayCommand(DeleteBookMethod);
            OpenWindowBook = new RelayCommand(OpenWindowBookMethod);
            AddAuthorToBook = new RelayCommand(AddAuthorToBookMethod);
            DeleteAuthorFromBook = new RelayCommand(DeleteAuthorFromBookMethod);
            EditBook = new RelayCommand(EditBookMethod);
        }

        public void AddAuthorMethod()
        {
            AllAuthors.Add(SelectedAuthor);
            Author OneAuthor = new Author();
            OneAuthor.FatherName = SelectedAuthor.FatherName;
            OneAuthor.FirstName = SelectedAuthor.FirstName;
            OneAuthor.SecondName = SelectedAuthor.SecondName;
            using (Model1 context = new Model1())
            {
                context.Authors.Add(OneAuthor);
                context.SaveChanges();
            }
        }

        public void AddBookMethod()
        {
            AllBooks.Add(BookName);
            Book NewBook = new Book();
            NewBook.BookName = BookName;
            using (Model1 context = new Model1())
            {
                context.Books.Add(NewBook);
                context.SaveChanges();
            }
        }

        public void OpenWindowAuthorMethod()
        {
                      
            if (CheckAuthor())
            {
                NewAuthor = SelectedAuthor;
                using (Model1 context = new Model1())
                {
                    ObservableCollection<Book> Books = context.Authors.Where(c => (c.SecondName == SelectedAuthor.SecondName) & (c.FatherName == SelectedAuthor.FatherName) & (c.FirstName == SelectedAuthor.FirstName)).Select(c => c.Books).FirstOrDefault();
                    foreach (Book b in Books)
                    {
                        BookOfAuthor.Add(b.BookName);
                    }
                }
                WindowAuthors WindowNew = new WindowAuthors();
                WindowNew.Show();
            }
            else MessageBox.Show("Автор не найден");
        }

        public void AddBookToAuthorMethod()
        {
            BookOfAuthor.Add(BookName);
            if (CheckBook()==false) AddBookMethod();
            Add(BookName,NewAuthor);
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
                Delete(name,NewAuthor);
            }
            else MessageBox.Show("Книга в списке отсутствует");
        }

        public void EditAuthorMethod()
        {
            using (Model1 context = new Model1())
            {
                Author OneAuthor = context.Authors.Where(c => (c.SecondName == NewAuthor.SecondName) & (c.FatherName == NewAuthor.FatherName) & (c.FirstName == NewAuthor.FirstName)).FirstOrDefault();
                OneAuthor.SecondName = SelectedAuthor.SecondName;
                OneAuthor.FirstName = SelectedAuthor.FirstName;
                OneAuthor.FatherName = SelectedAuthor.FatherName;
                context.SaveChanges();
            }
            NewAuthor = SelectedAuthor;
        }

        public void ShowAllAuthorsMethod()
        {
            using (Model1 context = new Model1())
            {
                foreach (Author a in context.Authors)
                {
                    AllAuthors.Add(new AuthorViewModel() { SecondName = a.SecondName, FirstName = a.FirstName, FatherName = a.FatherName });
                }
            }
        }

        public void DeleteAuthorMethod()
        {
            if (CheckAuthor())
            {
                using (Model1 context = new Model1())
                {
                    Author OneAuthor = context.Authors.Where(c => (c.SecondName == SelectedAuthor.SecondName) & (c.FatherName == SelectedAuthor.FatherName) & (c.FirstName == SelectedAuthor.FirstName))
                        .Include(c => c.Books).FirstOrDefault();
                    context.Authors.Remove(OneAuthor);
                    context.SaveChanges();
                }
                AllAuthors.Remove(SelectedAuthor);
            }
            else MessageBox.Show("Автор не найден");
        }

        public void ShowAllBooksMethod()
        {
            using (Model1 context = new Model1())
            {
                foreach (Book a in context.Books)
                {
                    AllBooks.Add(a.BookName);
                }
            }
        }

        public void DeleteBookMethod()
        {
            if(CheckBook())
            {
                using (Model1 context = new Model1())
                {
                    Book OneBook = context.Books.Where(c => c.BookName == BookName)
                        .Include(c => c.Authors).FirstOrDefault();
                    context.Books.Remove(OneBook);
                    context.SaveChanges();
                }
                AllBooks.Remove(BookName);
            }
            else MessageBox.Show("Книга не найдена");
        }

        public void OpenWindowBookMethod()
        {
            
            if (CheckBook())
            {
                if (AuthorsOfBook.Count != 0) AuthorsOfBook.Clear();
                using (Model1 context = new Model1())
                {
                    ObservableCollection<Author> Authors = context.Books.Where(c => (c.BookName == BookName)).Select(c => c.Authors).FirstOrDefault();
                    foreach (Author a in Authors)
                    {
                        AuthorsOfBook.Add(new AuthorViewModel() { SecondName = a.SecondName, FirstName = a.FirstName, FatherName = a.FatherName });
                    }
                }
                NameOfBook = BookName;
                SelectedAuthor = null;
                WindowBooks WindowNew = new WindowBooks();
                WindowNew.Show();
            }
            else MessageBox.Show("Книга не найдена");
        }
        public void AddAuthorToBookMethod()
        {
            AuthorsOfBook.Add(SelectedAuthor);
            if (CheckAuthor()==false) AddAuthorMethod();
            Add(NameOfBook,SelectedAuthor);
        }

        public void DeleteAuthorFromBookMethod()
        {
            Boolean proverca = false;
            foreach (AuthorViewModel b in AuthorsOfBook)
            {
                if ((b.SecondName == SelectedAuthor.SecondName)&(b.FatherName==SelectedAuthor.FatherName)&(b.FirstName==SelectedAuthor.FirstName)) proverca = true;
            }
            if (proverca == true)
            {
                Delete(NameOfBook,SelectedAuthor);
                AuthorsOfBook.Remove(SelectedAuthor);
            }
            else MessageBox.Show("Автор в списке отсутствует");
        }

        public void EditBookMethod()
        {
            using (Model1 context = new Model1())
            {
                Book OneBook = context.Books.Where(c => (c.BookName == NameOfBook)).FirstOrDefault();
                OneBook.BookName = BookName;
                context.SaveChanges();
            }
            NameOfBook = BookName;
            MessageBox.Show("Название книги изменено");
        }

        public void Add(string nameBook,AuthorViewModel author)
        {
            using (Model1 context = new Model1())
            {
                Book _Book = context.Books.Where(c => (c.BookName == nameBook)).FirstOrDefault();
                Author OneAuthor = context.Authors.Where(c => (c.SecondName == author.SecondName) & (c.FatherName == author.FatherName) & (c.FirstName == author.FirstName)).FirstOrDefault();
                OneAuthor.Books.Add(_Book);
                context.SaveChanges();
            }
        }

        public void Delete(string n,AuthorViewModel author)
        {
            using (Model1 context = new Model1())
            {
                Author OneAuthor = context.Authors.Where(c => (c.SecondName == author.SecondName) & (c.FatherName == author.FatherName) & (c.FirstName == author.FirstName))
                    .Include(c => c.Books).FirstOrDefault();
                Book book = OneAuthor.Books.First(c => c.BookName == n);
                OneAuthor.Books.Remove(book);
                context.SaveChanges();
            }
        }
        
        public bool CheckAuthor()
        {
            bool check = false;
            using (Model1 context = new Model1())
            {
                var OneAuthor = context.Authors.Where(c => (c.SecondName == SelectedAuthor.SecondName) & (c.FatherName == SelectedAuthor.FatherName) & (c.FirstName == SelectedAuthor.FirstName));
                if (OneAuthor.Count() != 0) check = true;
                return check;
            }
        }

        public bool CheckBook()
        {
            bool check = false;
            using (Model1 context = new Model1())
            {
                var OneBook = context.Books.Where(c => c.BookName == BookName);
                if (OneBook.Count() != 0) check = true;
                return check;
            }
        }
    }
}
