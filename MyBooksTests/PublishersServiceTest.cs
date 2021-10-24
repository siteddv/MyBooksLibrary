using Microsoft.EntityFrameworkCore;
using MyBooksLibrary.Data;
using MyBooksLibrary.Data.Models;
using MyBooksLibrary.Data.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBooksTests
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("PublishersDbTest")
            .Options;

        private AppDbContext _context;
        private PublishersService _publishersService;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            _publishersService = new PublishersService(_context);

            SeedDatabase();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test, Order(1)]
        public void GetAllPublishersWithEmptyParams()
        {
            var result = _publishersService.GetAllPublishers("", "", null);

            Assert.That(result.Count, Is.EqualTo(5));
            Assert.AreEqual(5, result.Count);
        }

        [Test, Order(2)]
        public void GetAllPublishersSecPage()
        {
            var result = _publishersService.GetAllPublishers("", "", 2);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.AreEqual(1, result.Count);
        }

        [Test, Order(3)]
        public void GetAllPublishersWithSearch()
        {
            var result = _publishersService.GetAllPublishers("", "3", null);

            Assert.AreEqual("Publisher 3", result.FirstOrDefault().Name);
        }

        [Test, Order(4)]
        public void GetAllPublishersWithSort()
        {
            var result = _publishersService.GetAllPublishers("name_desc", "", null);

            Assert.AreEqual("Publisher 6", result.FirstOrDefault().Name);
        }

        [Test, Order(5)]
        public void GetPublisherByIdCorrect()
        {
            var result = _publishersService.GetPublisherById(1);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(1, result.Id);
        }

        [Test, Order(6)]
        public void GetPublisherByIdIncorrect()
        {
            var result = _publishersService.GetPublisherById(10);

            Assert.AreEqual(null, result);
        }

        [Test, Order(7)]
        public void GetPublisherByIdNegative()
        {
            var result = _publishersService.GetPublisherById(-10);

            Assert.AreEqual(null, result);
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    },
            };
            _context.Publishers.AddRange(publishers);

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            _context.Authors.AddRange(authors);


            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                }
            };
            _context.Books.AddRange(books);

            var books_authors = new List<Book_Author>()
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                },
            };
            _context.Book_Authors.AddRange(books_authors);


            _context.SaveChanges();
        }
    }
}