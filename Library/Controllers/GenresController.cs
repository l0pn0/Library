using Library.DataBaseContext;
using Library.Model;
using Library.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : Controller
    {
        private readonly LibraryApiDB _context;

        public GenresController(LibraryApiDB context)
        {
            _context = context;
        }

        [HttpPost("createNewGenre")]
        public async Task<IActionResult> CreateNewGenre([FromQuery] CreateNewGenre newGenre)
        {
            if (newGenre == null || string.IsNullOrEmpty(newGenre.Name_genre))
            {
                return BadRequest("Данные жанра отсутствуют или недействительны.");
            }

            var genre = new Genres
            {
                Title = newGenre.Name_genre
            };

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllGenres), new { id_genre = genre.Id_Genres }, genre);
        }

        [HttpGet("getAllGenres")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _context.Genres.ToListAsync();
            return Ok(new { genres, status = true });
        }

        [HttpPut("UpdateGenre/{id_genre}")]
        public async Task<IActionResult> UpdateGenre(int id_genre, [FromQuery] UpdateGenres updateGenres)
        {
            if (updateGenres == null || string.IsNullOrEmpty(updateGenres.Name_genre))
            {
                return BadRequest("Данные для обновления отсутствуют или недействительны.");
            }

            var genre = await _context.Genres.FindAsync(id_genre);
            if (genre == null)
            {
                return NotFound($"Жанр с ID {id_genre} не найден.");
            }

            genre.Title = updateGenres.Name_genre;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteGenre/{id_genre}")]
        public async Task<IActionResult> DeleteGenre(int id_genre)
        {
            var genre = await _context.Genres.FindAsync(id_genre);
            if (genre == null)
            {
                return NotFound($"Жанр с ID {id_genre} не найден.");
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}