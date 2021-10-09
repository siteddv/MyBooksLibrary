using MyBooksLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBooksLibrary.Data.ViewModels
{
    public class CustomActionResultViewModel
    {
        public Exception Exception { get; set; }
        public Publisher Publisher { get; set; }
    }
}
