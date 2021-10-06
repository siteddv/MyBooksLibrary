using MyBooksLibrary.Data.Models;
using MyBooksLibrary.Data.ViewModels;
using MyBooksLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public Publisher AddPublisher(PublisherViewModel publisher)
        {
            if (StringStartsWithNumber(publisher.Name))
                throw new PublisherNameException("NameStartsWithNumber", publisher.Name);

            var publisherModel = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(publisherModel);
            _context.SaveChanges();

            return publisherModel;
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(p => p.Id == id);
        
        public PublisherWithBooksAndAuthorsViewModel GetPublisherDataById(int publisherId)
        {
            var publisherData = _context.Publishers
                .Where(publisher => publisher.Id == publisherId)
                .Select(publisher => new PublisherWithBooksAndAuthorsViewModel()
                {
                    Name = publisher.Name,
                    BookAuthors = publisher.Books.Select(book => new BookAuthorViewModel()
                    {
                        BookName = book.Title,
                        BookAuthors = book.Book_Authors.Select(ba => ba.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(publisher => publisher.Id == id);

            if(publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id: {id} doesn't exist");
            }
        }

        private bool StringStartsWithNumber(string number) => Regex.IsMatch(number, @"^\d");
    }
}
