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
        public Book UpdateBookById(int bookId, BookViewModel book)
        {
            var bookModel = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if(bookModel != null)
            {
                bookModel.Title = book.Title;
                bookModel.Description = book.Description;
                bookModel.IsRead = book.IsRead;
                bookModel.DateRead = book.IsRead ? book.DateRead.Value : null;
                bookModel.Rate = book.IsRead ? book.Rate : null;
                bookModel.Genre = book.Genre;
                bookModel.Author = book.Author;
                bookModel.CoverUrl = book.CoverUrl;

                _context.SaveChanges();
            }

            return bookModel;
        }
    }
}
