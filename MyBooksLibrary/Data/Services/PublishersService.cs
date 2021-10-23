using MyBooksLibrary.Data.Models;
using MyBooksLibrary.Data.Paginig;
using MyBooksLibrary.Data.ViewModels;
using MyBooksLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyBooksLibrary.Data.Services
{
    public class PublishersService
    {
        private readonly AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }
        public Publisher AddPublisher(PublisherViewModel publisher)
        {
            var publisherModel = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(publisherModel);
            _context.SaveChanges();

            return publisherModel;
        }

        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber) 
        {
            
            var allPublishers = _context.Publishers.OrderBy(p => p.Name).ToList();

            if (!string.IsNullOrEmpty(sortBy) && sortBy.Equals("name_desc"))
                allPublishers = allPublishers.OrderByDescending(p => p.Name).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                allPublishers = allPublishers.Where(p => p.Name.ToLower().Contains(searchString)).ToList();
            }

            int pageSize = 2;
            allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);
            
            return allPublishers;
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
                throw new ArgumentNullException($"The publisher with id: {id} doesn't exist");
            }
        }

        private bool StringStartsWithNumber(string number) => Regex.IsMatch(number, @"^\d");
    }
}
