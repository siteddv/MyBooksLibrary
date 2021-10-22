﻿using System.Collections.Generic;

namespace MyBooksLibrary.Data.ViewModels
{
    public class PublisherViewModel
    {
        public string Name { get; set; }
    }

    public class PublisherWithBooksAndAuthorsViewModel
    {
        public string Name { get; set; }
        public List<BookAuthorViewModel> BookAuthors { get; set; }
    }

    public class BookAuthorViewModel
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}
