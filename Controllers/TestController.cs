// using BookStoreApi.Models;
// using BookStoreApi.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace BookStoreApi.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class TestController : ControllerBase
//     {
//         private readonly BooksService _booksService;

//         public TestController(BooksService booksService) =>
//             _booksService = booksService;

//         [HttpGet("test-db-connection")]
       
// public async Task<IActionResult> TestDbConnection()
// {
//     try
//     {
//         var books = await _booksService.GetAsync();
//         if (books == null || !books.Any())
//         {
//             return NotFound("No books found.");
//         }
//         return Ok(books);
//     }
//     catch (Exception ex)
//     {
//         return StatusCode(500, $"Internal server error: {ex.Message}");
//     }
// }

//     }
// }
using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly BooksService _booksService;
        private readonly ILogger<TestController> _logger;

        public TestController(BooksService booksService, ILogger<TestController> logger) =>
            (_booksService, _logger) = (booksService, logger);

        [HttpGet("test-db-connection")]
        public async Task<IActionResult> TestDbConnection()
        {
            try
            {
                _logger.LogInformation("Testing DB Connection...");
                var books = await _booksService.GetAsync();
                if (books == null || !books.Any())
                {
                    _logger.LogInformation("No books found.");
                    return NotFound("No books found.");
                }
                _logger.LogInformation($"Found {books.Count} books.");
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
