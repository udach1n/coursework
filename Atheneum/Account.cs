using System.Collections.Generic;
using Atheneum.Book;


namespace Atheneum.Accounts
{
    public class Account
    {
        private List<Books> booksTaken;
        public Account(int id, string login,string password)
        {
            ID = id;
            Login = login;
            Password = password;

            booksTaken = new List<Books>();
        }
        public string Password { get; private set; }
        public int ID { get; private set; }
        public string Login { get; private set; }

        public List<Books> BooksTaken => new List<Books>(booksTaken);
        public void TakeBook(Books book)
        {
            if (book is Books)
            {
                booksTaken.Add(new Books(book));
                booksTaken.Sort();
            }
        }
        public Books ReturnBook(int id)
        {
            Books returnBook = null;
                if (booksTaken.Exists(book => book is Books && book.ID == id))
                {
                returnBook = new Books(booksTaken.Find(book => book is Books && book.ID == id));
                booksTaken.Remove(booksTaken.Find(book => book is Books && book.ID == id));
                }
            
            return returnBook;
        }
    }
}