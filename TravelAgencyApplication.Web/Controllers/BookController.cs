using Microsoft.AspNetCore.Mvc;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

        [Route("Details/{id?}")]
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = _bookService.GetDetailsForBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
    }
}
