using MyBooksLibrary.Data.Models;
using MyBooksLibrary.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBooksLibrary.Data.Services
{
    public class BookService
    {
        private AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }
        public void AddBook(BookViewModel book)
        {
            var bookModel = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                Author = book.Author,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };
            _context.Books.Add(bookModel);
            _context.SaveChanges();
        }
        public List<Book> GetAllBooks() => _context.Books.ToList();
        public Book GetBookById(int bookId) => _context.Books.FirstOrDefault(n => n.Id == bookId);
    }
}
