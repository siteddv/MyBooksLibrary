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
        public void AddBookWithAuthors(BookViewModel book)
        {
            var bookModel = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };
            _context.Books.Add(bookModel);
            _context.SaveChanges();
            foreach(var authorId in book.AuthorIds)
            {
                var bookAuthor = new Book_Author()
                {
                    BookId = bookModel.Id,
                    AuthorId = authorId
                };
                _context.Book_Authors.Add(bookAuthor);
                _context.SaveChanges();
            }
        }
        public List<Book> GetAllBooks() => _context.Books.ToList();
        public BookWithAuthorsViewModel GetBookById(int bookId)
        {
            var bookWithAuthors = _context.Books
                .Where(book => book.Id == bookId)
                .Select(book => new BookWithAuthorsViewModel()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(ba => ba.Author.FullName).ToList()
            }).FirstOrDefault();

            return bookWithAuthors;
        }
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
                bookModel.CoverUrl = book.CoverUrl;

                _context.SaveChanges();
            }

            return bookModel;
        }
        public void DeleteBookById(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if(book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
