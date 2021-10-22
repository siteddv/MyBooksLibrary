using MyBooksLibrary.Data.Models;
using MyBooksLibrary.Data.ViewModels;
using System.Linq;

namespace MyBooksLibrary.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;

        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }
        public void AddAuthor(AuthorViewModel author)
        {
            var authorModel = new Author()
            {
                FullName = author.FullName
            };
            _context.Authors.Add(authorModel);
            _context.SaveChanges();
        }

        public AuthorWithBooksViewModel GetAuthorWithBooks(int authorId)
        {
            var author = _context.Authors
                .Where(author => author.Id == authorId)
                .Select(author => new AuthorWithBooksViewModel()
            {
                FullName = author.FullName,
                BookTitles = author.Book_Authors.Select(a => a.Book.Title).ToList()
            }).FirstOrDefault();

            return author;
        }
    }
}
