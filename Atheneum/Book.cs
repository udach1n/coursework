using System;


namespace Atheneum.Book
{
    public class Books : IComparable<Books>
    {
        public Books(Books Book)
            : this(Book.ID, Book.Title, Book.Author, Book.Genre, Book.Available)
        {}
        public Books(int id, string title, string author, GenreBooks genre, Availability available)
        {
            ID = id;
            Title = title;
            Author = author;
            Genre = genre;
            Available = available;
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Availability Available { get; set; }
        public GenreBooks Genre { get; set; }
        public int CompareTo(Books comparetoBook)
        {
            if (comparetoBook == null)
                return 0;
            else
                return ID.CompareTo(comparetoBook.ID);
        }
        public  override string ToString()
        {
            return $"{ID,3} {Title,-30} {Author,-20} {Genre,-20}{Available,-20}";
        }
    }
}