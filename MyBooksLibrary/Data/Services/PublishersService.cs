using MyBooksLibrary.Data.Models;
using MyBooksLibrary.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBooksLibrary.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }
        public void AddPublisher(PublisherViewModel publisher)
        {
            var publisherModel = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(publisherModel);
            _context.SaveChanges();
        }
        public List<Book> GetAllBooks() => _context.Books.ToList();
        public Book GetBookById(int bookId) => _context.Books.FirstOrDefault(n => n.Id == bookId);
        public Book UpdateBookById(int bookId, BookViewModel book)
        {
            var bookModel = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (bookModel != null)
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
        public void DeleteBookById(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
