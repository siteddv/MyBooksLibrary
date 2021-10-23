using MyBooksLibrary.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyBooksLibrary.Data.Services
{
    public class LogsService
    {
        private readonly AppDbContext _context;
        public LogsService(AppDbContext context)
        {
            _context = context;
        }

        public List<Log> GetAllLogs() => _context.Logs.ToList();
    }
}
