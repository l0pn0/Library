using Library.DataBaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Library.Model;
using Library.Requests;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly LibraryApiDB _context;

        public BooksController(LibraryApiDB context)
        {
            _context = context;
        }

        [HttpGet("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _context.Book.ToListAsync();
            return Ok(new { books, status = true });
        }

        [HttpGet("FindById/{id_book}")]
        public async Task<IActionResult> FindById(int id_book)
        {
            var book = await _context.Book.FindAsync(id_book);
            if (book == null)
            {
                return NotFound($"Book with ID {id_book} not found.");
            }

            var bookDto = new Find.GetAllBooksId
            {
                Id_Books = book.Id_Book,
                Title = book.Title,
                Author = book.Author,
                Year_public = book.YearOfPublication,
                Id_genre = book.Genre_Id,
                Description = book.Description,
                Copies = book.AvailableCopies
            };
            return Ok(bookDto);
        }

        [HttpGet("FindByIdGenre/{id_genre}")]
        public async Task<IActionResult> FindByIdGenre(int id_genre)
        {
            var books = await _context.Book.Where(b => b.Genre_Id == id_genre).ToListAsync();

            var booksDto = books.Select(b => new Find.GetAllBooksId
            {
                Id_Books = b.Id_Book,
                Title = b.Title,
                Author = b.Author,
                Year_public = b.YearOfPublication,
                Id_genre = b.Genre_Id,
                Description = b.Description,
                Copies = b.AvailableCopies
            });

            return Ok(booksDto);
        }

        [HttpPost("createNewBook")]
        public async Task<IActionResult> CreateNewBook([FromQuery] CreateNewBook newBook)
        {
            if (newBook == null)
            {
                return BadRequest("New book data is null.");
            }

            var book = new Book
            {
                Title = newBook.Title,
                Author = newBook.Author,
                //?
                YearOfPublication = newBook.Year_public,
                Genre_Id = newBook.Genre_Id,
                Description = newBook.Description,
                AvailableCopies = newBook.AvailableCopies
            };

            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(FindById), new { id_book = book.Id_Book }, book);
        }

        [HttpDelete("DeleteBook/{id_book}")]
        public async Task<IActionResult> DeleteBook(int id_book)
        {
            var book = await _context.Book.FindAsync(id_book);
            if (book == null)
            {
                return NotFound($"Book with ID {id_book} not found.");
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("UpdateBook/{id_book}")]
        public async Task<IActionResult> UpdateBook(int id_book, [FromQuery] UpdateBooks updBook)
        {
            if (updBook == null)
            {
                return BadRequest("Update data is null.");
            }

            var book = await _context.Book.FindAsync(id_book);
            if (book == null)
            {
                return NotFound($"Book with ID {id_book} not found.");
            }

            book.Title = updBook.Title;
            book.Author = updBook.Author;
            book.YearOfPublication = updBook.Year_public;
            book.Description = updBook.Description;
            book.AvailableCopies = updBook.Copies;
            book.Genre_Id = updBook.Id_genre;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("FindByAuthor/{authorOrTitle}")]
        public async Task<IActionResult> SearchBooks(string authorOrTitle)
        {
            var books = await _context.Book
                .Where(b => b.Title.Contains(authorOrTitle) || b.Author.Contains(authorOrTitle))
                .ToListAsync();

            if (!books.Any())
            {
                return NotFound($"No books found for query '{authorOrTitle}'.");
            }

            var booksDto = books.Select(b => new CreateNewBook
            {
                Title = b.Title,
                Author = b.Author,
                Genre_Id = b.Genre_Id,
                AvailableCopies = b.AvailableCopies,
                Year_public = b.YearOfPublication,
                Description = b.Description
            });

            return Ok(booksDto);
        }

        [HttpGet("FindCopies/{title}")]
        public async Task<IActionResult> FindCopies(string title)
        {
            var books = await _context.Book.Where(b => b.Title == title).ToListAsync();

            if (!books.Any())
            {
                return NotFound($"No copies found for book title '{title}'.");
            }

            var booksDto = books.Select(b => new Find.FindCopies
            {
                Copies = b.AvailableCopies
            });

            return Ok(booksDto);
        }
    }
}