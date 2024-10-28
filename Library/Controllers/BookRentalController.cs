using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Requests;
using Library.DataBaseContext;
using Library.Model;
using LessonApiBiblioteka.Requests;

[ApiController]
[Route("api/[controller]")]
public class BookRentalController : ControllerBase
{
    private readonly LibraryApiDB _context;

    public BookRentalController(LibraryApiDB context)
    {
        _context = context;
    }

    [HttpPost("rent")]
    public async Task<IActionResult> RentBook([FromQuery] RentBookRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Данные запроса неверные.");
        }

        try
        {
            var book = await _context.Book.FindAsync(request.Id_book);
            if (book == null)
            {
                return NotFound("Книга не найдена.");
            }

            if (book.AvailableCopies <= 0)
            {
                return BadRequest("Нет доступных копий для аренды.");
            }

            var reader = await _context.Readers.FindAsync(request.Id_reader);
            if (reader == null)
            {
                return NotFound("Читатель не найден.");
            }

            var rental = new RentalHistory
            {
                book_id = request.Id_book,
                reader_id = reader.Id_Readers,
                rental_date = DateTime.UtcNow,
                DueDate = request.DueDate
            };

            book.AvailableCopies--;

            _context.RentalHistory.Add(rental);
            await _context.SaveChangesAsync();

            return Ok("Книга арендована.");
        }
        catch (DbUpdateException ex)
        {
            // Логирование внутреннего исключения
            Console.WriteLine(ex.InnerException?.Message);
            return StatusCode(500, $"Ошибка базы данных: {ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка сервера: {ex.Message}");
        }
    }

    [HttpPost("return")]
    public async Task<IActionResult> ReturnBook([FromQuery] ReturnBookRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Данные запроса неверные.");
        }

        try
        {
            var rental = await _context.RentalHistory.FindAsync(request.RentalId);
            if (rental == null)
            {
                return NotFound("Аренда не найдена.");
            }

            var book = await _context.Book.FindAsync(rental.book_id);
            if (book == null)
            {
                return NotFound("Книга не найдена.");
            }

            rental.return_date = DateTime.UtcNow;
            book.AvailableCopies++;

            await _context.SaveChangesAsync();

            return Ok("Книга возвращена.");
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Ошибка базы данных: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка сервера: {ex.Message}");
        }
    }

    [HttpGet("user/{id_reader}/history")]
    public async Task<IActionResult> GetRentalHistoryByUser(int id_reader)
    {
        try
        {
            var rentals = await _context.RentalHistory
                .Where(r => r.reader_id == id_reader)
                .Include(r => r.Book)
                .Include(r => r.Reader)
                .Select(r => new
                {
                    BookTitle = r.Book.Title,
                    UserName = r.Reader.Name + " " + r.Reader.Surname,
                    RentalDate = r.rental_date,
                    DueDate = r.DueDate,
                    ReturnDate = r.return_date
                })
                .ToListAsync();

            if (rentals == null || rentals.Count == 0)
            {
                return NotFound("История аренды не найдена.");
            }

            return Ok(rentals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка сервера: {ex.Message}");
        }
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentRentals()
    {
        try
        {
            var rentals = await _context.RentalHistory
                .Where(r => r.return_date == null)
                .Include(r => r.Book)
                .Include(r => r.Reader)
                .Select(r => new
                {
                    BookTitle = r.Book.Title,
                    UserName = r.Reader.Name+ " " + r.Reader.Surname,
                    RentalDate = r.rental_date,
                    DueDate = r.DueDate
                })
                .ToListAsync();

            return Ok(rentals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка сервера: {ex.Message}");
        }
    }

    [HttpGet("book/{id_book}/history")]
    public async Task<IActionResult> GetRentalHistoryByBook(int id_book)
    {
        try
        {
            var rentals = await _context.RentalHistory
                .Where(r => r.book_id == id_book)
                .Include(r => r.Book)
                .Include(r => r.Reader)
                .Select(r => new
                {
                    BookTitle = r.Book.Title,
                    UserName = r.Reader.Name + " " + r.Reader.Surname,
                    RentalDate = r.rental_date,
                    DueDate = r.DueDate,
                    ReturnDate = r.return_date
                })
                .ToListAsync();

            if (rentals == null || rentals.Count == 0)
            {
                return NotFound("История аренды для книги не найдена.");
            }

            return Ok(rentals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка сервера: {ex.Message}");
        }
    }
}