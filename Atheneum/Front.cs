using System;
using System.Collections.Generic;
using Atheneum.Exceptions;
using Atheneum.Book;
using Atheneum.Accounts;
using Atheneum.Library;


namespace Front
{

    public static class Appearance
    {
        private static Library library;
        public static string LibraryName => library.Name;

        public static void MakeLibrary(string name)
        {
            library = new Library(name);
        }
        private static bool ListBooks()
        {
            List<Books> bookList = library.BookList;            
            Console.WriteLine("\nAll available books:");
            Console.WriteLine($"{"ID",3} {"Title",-30} {"Author",-30} {"Genre",-20}{"Availability"}");
            foreach (Books book in bookList)
            {
                Console.WriteLine(book.ToString());
            }
            return true;
        }
        private static void ListBooks(List<Books> books)
        {
            Console.WriteLine($"{"ID",3} {"Title",-30} {"Author",-33} {"Genre",-15}{"Availability"}");
            foreach (Books book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }
        private static bool CreateUserAccount(string login, string passsword)
        {
            bool creationSuccessful = false;
            try
            {
                library.CreateUserAccount(login, passsword);
                creationSuccessful = true;
                Console.WriteLine("New account created");
            }
            catch (IncorrectLoginException e)
            {
                ErrorMessage(e.Message);
            }
            return creationSuccessful;
        }
        private static void TakeBook(int userID, int bookID, string password,string login)
        {
            try
            {
                library.TakeBook(userID, bookID, password, login);
            }
            catch (LibraryException e)
            {
                ErrorMessage(e.Message);
            }            
        }
        private static void ReturnBook(int userID, int bookID, string password,string login)
        {
            try
            {
                library.ReturnBook(userID, bookID, password, login);
            }
            catch (LibraryException e)
            {
                ErrorMessage(e.Message);
            }
        }
        private static void ListAllBooksTaken(int userID, string password,string login)
        {
            Account user = library.FindAccount(userID, password, login);
                if (user.BooksTaken.Count == 0)
                {
                    Console.WriteLine("No books taken");
                }
                else
                {
                    Console.WriteLine($"\nAll books taken by user {user.Login}:");
                    Console.WriteLine($"{"ID",3} {"Title",-30} {"Author",-30} {"Genre ",-20}{"Availability"}");
                    foreach (Books book in user.BooksTaken)
                    {
                        if (book is Books)
                        {
                            Console.WriteLine(book.ToString());
                        }
                    }
                    Console.WriteLine();
                }
        }
        private static void ErrorMessage(string message)
        {
            Console.WriteLine(message);
        }



        public static void Menu()
        {
            bool startMenuWorking = true;
            while (startMenuWorking == true)
            {
                Console.WriteLine("\t" + LibraryName);
                Console.WriteLine("Select number:\n" + "1. Login\n" + "2. Register\n" +
                    "3. Book search\n" + "4. List of available books\n" + "5. Exit\n");

                Console.Write("Сhoose: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter the ID of the registered user: ");
                        int userID = int.Parse(Console.ReadLine());
                        Console.Write("Enter the login of the registered user: ");
                        string login = Console.ReadLine();
                        Console.Write("Enter the password of the registered user: ");
                        string password = Console.ReadLine();
                        UserMenu(userID, password,login);
                        break;
                    case 2:
                        Console.Write("Enter new login: ");
                        string newlogin = Console.ReadLine();
                        Console.Write("Enter new password: ");
                        string newpassword = Console.ReadLine();
                        CreateUserAccount(newlogin, newpassword);
                        break;
                    case 3:
                        Search();
                        break;
                    case 4:
                        ListBooks();
                        break;
                    case 5:
                        startMenuWorking = false;
                        break;
                    default:
                        ErrorMessage("Wrong choice input. No such number exists. Should be integer 1 to 5");
                        break;
                }
            }
        }
        private static void UserMenu(int userID, string password,string login)
        {
            if (userID > 0)
            {
                Account user = library.FindAccount(userID, password,login);
                if (user != null)
                {
                    bool usersmenu = true;
                    while (usersmenu == true)
                    {
                        Console.WriteLine($"\n ID: {user.ID}\tUser: {user.Login} \tPassword: {user.Password}");
                        Console.WriteLine("Select number:\n" + "1. Select book\n" + "2. Return a book\n" + "3. Book search\n" +
                            "4. List of all borrowed books\n" + "5. List of available books\n" + "6. Exit account\n");
                        Console.Write("Your choice: ");
                        int select = int.Parse(Console.ReadLine());
                        switch (select)
                        {
                            case 1:
                                ListBooks();
                                Console.Write("\n" + " Select number  ");
                                int bookID = int.Parse(Console.ReadLine());
                                TakeBook(userID, bookID, password, login);
                                break;
                            case 2:
                                if (user.BooksTaken.Count == 0)
                                {
                                    Console.WriteLine("Nothing to return");
                                    break;
                                }
                                else
                                {
                                    ListAllBooksTaken(userID, password, login);
                                }
                                Console.Write("Enter ID book: ");
                                int bookId = int.Parse(Console.ReadLine());
                                ReturnBook(userID, bookId, password, login);
                                break;
                            case 3:
                                Search();
                                break;
                            case 4:
                                ListAllBooksTaken(userID, password, login);
                                break;
                            case 5:
                                ListBooks();
                                break;
                            case 6:
                                usersmenu = false;
                                break;
                            default:
                                ErrorMessage("There is no such number. Must be 1 to 6");
                                break;
                        }
                    }
                }
                else
                {
                    ErrorMessage("There is no such user");
                }
            }
            else
            {
                ErrorMessage("Wrong user ID input. Must be more than 0");
            }
        }
        private static void Search()
        {
            Console.WriteLine("\nSelect:\n" + "1. Title\n" + "2. ID\n" + "3. Author\n");
            Console.Write("Select number: ");
            int choiceSearchParameter = int.Parse(Console.ReadLine());
            switch (choiceSearchParameter)
            {
                case 1:
                    Console.Write("Enter book title: ");
                    string title = Console.ReadLine();
                    List<Books> foundBook = library.SearchBooksTitle(title);
                    if (foundBook.Count == 0)
                    {
                        ErrorMessage("No books found");
                    }
                    else
                    {
                        Console.WriteLine("\nBooks found: ");
                        ListBooks(foundBook);
                    }
                    break;
                case 2:
                    Console.Write("Enter books ID: ");
                    int bookID = int.Parse(Console.ReadLine());
                    foundBook = library.SearchBooksID(bookID);
                    if (foundBook.Count == 0)
                    {
                        ErrorMessage("No books found");
                    }
                    else
                    {
                        Console.WriteLine("\nBooks found: ");
                        ListBooks(foundBook);
                    }
                    break;
                case 3:
                    Console.Write("Enter author: ");
                    string author = Console.ReadLine();
                    foundBook = library.SearchBooksAuthor(author);
                    if (foundBook.Count == 0)
                    {
                        ErrorMessage("No books found");
                    }
                    else
                    {
                        Console.WriteLine("\nBooks found: ");
                        ListBooks(foundBook);
                    }
                    break;
                default:
                    ErrorMessage("Wrong choice input. Must be  1 to 3");
                    break;
            }

        }
    }
}