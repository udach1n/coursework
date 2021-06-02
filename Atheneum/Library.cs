using System.Collections.Generic;
using Atheneum.Book;
using Atheneum.Accounts;
using Atheneum.Exceptions;


namespace Atheneum.Library
{
    public class Library
    {
        private int UsersCount = 0; 
        private const int MinLoginLength = 5; 
        private const int MaxLoginLength = 20; 
        private const int MinPasswordLength = 6; 
        private const int MaxPasswordLength = 30; 

        private List<Books> Bookslist; 
        private List<Account> Accountslist; 

        public Library(string namelibrary)
        {
            Name = namelibrary;
            Bookslist = new List<Books>();
            Accountslist = new List<Account>();

            AddBook(new Books(1, "Country Ukraine", ("Pip Pop"), GenreBooks.Roman, Availability.out_of_stock));
            AddBook(new Books(2, "The Children of Captain Grant", ("Jules Verne"), GenreBooks.Children, Availability.in_of_stock));
            AddBook(new Books(3, "Toreador from Vasyukovka", ("Vsevolod Nestayko"), GenreBooks.Children, Availability.in_of_stock));
            AddBook(new Books(4, "War and Peace", ("Lev Tolstoy"), GenreBooks.Adventures, Availability.in_of_stock));
            AddBook(new Books(5, "round planet", ("Rock Paper"), GenreBooks.Fantasy, Availability.out_of_stock));
            AddBook(new Books(6, "Selfish gene", ("Richard Dawkins"), GenreBooks.Science, Availability.in_of_stock));
            AddBook(new Books(7, "God as an illusion", ("Richard Dawkins"), GenreBooks.Science, Availability.in_of_stock));
            AddBook(new Books(8, "Animals of the North", ("Dieter Brown"), GenreBooks.Thriller, Availability.in_of_stock));
            AddBook(new Books(9, "Adventures of Tom Sawyer", ("Mark Twain"), GenreBooks.Children, Availability.out_of_stock));
            AddBook(new Books(10, "Jane Eyre", ("Charlotte Bronte"), GenreBooks.Thriller, Availability.in_of_stock));
            AddBook(new Books(11, "Archive", ("Rar Zip"), GenreBooks.Roman, Availability.in_of_stock));
            AddBook(new Books(12, "Hello world", ("Vasya Pupkin"), GenreBooks.Adventures, Availability.out_of_stock));
            AddBook(new Books(13, "Attachment", ("Visual Studio"), GenreBooks.Science, Availability.in_of_stock));
            AddBook(new Books(14, "Google", ("Explorer Opera"), GenreBooks.Adventures, Availability.in_of_stock));
            AddBook(new Books(15, "Movie", ("Screen Choice"), GenreBooks.Roman, Availability.in_of_stock));




        }

        public string Name { get; private set; }
        public List<Books> BookList => new List<Books>(Bookslist);
       
        public List<Account> AccountList => new List<Account>(Accountslist);

        public void AddBook(Books book)
        {
            Bookslist.Add(new Books(book.ID, book.Title, book.Author, book.Genre, book.Available));
        }
        public void TakeBook(int userID, int bookID, string password,string login)
        {
            if (bookID > 0 && bookID < 16)
            {

                if (Bookslist.Exists(x => x.ID == bookID))
                {
                    Books book = Bookslist.Find(x => x.ID == bookID);
                    Account user = FindAccount(userID, password, login);
                            if (user.BooksTaken.Count < 10 && book.Available == Availability.in_of_stock)
                            {
                                user.TakeBook(book);
                                Bookslist.Remove(book);
                            }

                            else
                                throw new LibraryException("You have taken the maximum of books (10) or unavailable book. return the book to get a new one");
                    }
                    else
                        throw new LibraryException("Someone took this book");
                }
                else
                    throw new LibraryException("Wrong ID of books");
            
        }
        public void ReturnBook(int userID, int bookID, string password,string login)
        {
            Account user = FindAccount(userID, password, login);
            if (user.BooksTaken.Count == 0)
                throw new LibraryException("Nothing to return");
            Books returnBook = user.ReturnBook(bookID);            

                if (returnBook != null)
                {
                    if (returnBook is Books)
                    {
                        Bookslist.Add(returnBook);
                        Bookslist.Sort();
                    }
                }
                else
                    throw new LibraryException("User didn't take such book");
        }
        public void CreateUserAccount(string login,string password)
        {
            if (login.Length < MinLoginLength || login.Length > MaxLoginLength)
            {
                throw new IncorrectLoginException($"incorrect long length. Must be between 5 and 20 characters");
            }
            else if (password.Length < MinPasswordLength || password.Length > MaxPasswordLength)
            {
                throw new IncorrectLoginException($"Inappropriate password length. Must be between 6 and 30 characters");
            }
            else if (Accountslist.Exists(account => account.Login == login))
            {
                throw new IncorrectLoginException("An account with such login already exists");
            }            
            else
            {
                UsersCount++;
                Accountslist.Add(new Account(UsersCount, login,password));
            }
        }
        public Account FindAccount(int userID,string password,string login)
        {
            foreach (Account user in Accountslist)
            {
                if (user.ID == userID && user.Password == password && user.Login == login)
                {
                    return user;
                }               
            }
            return null;
        }
        public List<Books> SearchBooksID(int id)
        {
            List<Books> foundBooks = new List<Books>();
            foundBooks.AddRange(Bookslist.FindAll(book => book.ID == id));
            return foundBooks;
        }
        public List<Books> SearchBooksTitle(string title)
        {
            List<Books> foundBooks = new List<Books>();
            foundBooks.AddRange(Bookslist.FindAll(book => book.Title.Contains(title)));
            return foundBooks;
        }
        public List<Books> SearchBooksAuthor(string author)
        {
            List<Books> foundBooks = new List<Books>();
            foundBooks.AddRange(Bookslist.FindAll(book => book.Author.Contains(author)));
            return foundBooks;
        }
    }
}