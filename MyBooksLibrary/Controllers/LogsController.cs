using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBooksLibrary.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBooksLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LogsService _logsService;

        public LogsController(LogsService logsService)
        {
            _logsService = logsService;
        }

        [HttpGet("get-all-logs")]
        public IActionResult GetAllLogs()
        {
            try
            {
                var allLogs = _logsService.GetAllLogs();
                return Ok(allLogs);
            }
            catch
            {
                return BadRequest("Could not load logs from db");
            }
        }
    }
}
