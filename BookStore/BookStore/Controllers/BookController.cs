namespace BookStore.Controllers
{
    using System.Collections.Generic;
    using BookStore.Models;
    using BookStore.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            return _bookService.Get();
        }

        [HttpGet("moreone")]
        public ActionResult<List<string>> GetMoreOne()
        {
            return _bookService.GetMoreOne();
        }

        [HttpGet("min")]
        public ActionResult<Book> GetWithMin()
        {
            return _bookService.GetWithMin();
        }

        [HttpGet("max")]
        public ActionResult<Book> GetWithMax()
        {
            return _bookService.GetWithMax();
        }

        [HttpGet("wauthor")]
        public ActionResult<List<Book>> GetWithoutAuthor()
        {
            return _bookService
                .GetWithoutAuthor();
        }

        [HttpGet("authors")]
        public ActionResult<List<string>> GetAuthors()
        {
            return _bookService.GetAuthors();
        }

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPost("add")]
        public ActionResult<List<Book>> Create()
        {
            var books = new List<Book> {
                new Book { Name = "Hobbit", Author = "Tolkien", Count = 5, Genre = new List<string> { "Fantasy" }, Year = 2014  },
                new Book { Name = "Lord of the rings", Author = "Tolkien", Count = 3, Genre = new List<string> { "Fantasy" }, Year = 2015  },
                new Book { Name = "Kolobok", Count = 10, Genre = new List<string> { "Kids" }, Year = 2000  },
                new Book { Name = "Repka", Count = 11, Genre = new List<string> { "Kids" }, Year = 2000  },
                new Book { Name = "Dyadya Stiopa", Author = "Mihalkov", Count = 1, Genre = new List<string> { "Kids" }, Year = 2001  }
            };

            _bookService.CreateMany(books);

            return Created("GetBook", books);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);

            return NoContent();
        }

        [HttpPut("increment")]
        public IActionResult IncrementAll()
        {
            var books = _bookService.Get();

            if (books == null)
            {
                return NotFound();
            }

            _bookService.IncrementAll();

            return NoContent();
        }

        [HttpPut("favority")]
        public IActionResult PushFavority()
        {
            var books = _bookService.Get();

            if (books == null)
            {
                return NotFound();
            }

            _bookService.PushFavorityIfNotExists();

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }

        [HttpDelete("lessthree")]
        public IActionResult DeleteLessThree()
        {
            _bookService.RemoveAllLessThree();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteAll()
        {
            _bookService.RemoveAll();

            return NoContent();
        }
    }
}
