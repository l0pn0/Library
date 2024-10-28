using Library.DataBaseContext;
using Library.Model;
using Library.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : Controller
    {
        private readonly LibraryApiDB _context;

        public ReadersController(LibraryApiDB context)
        {
            _context = context;
        }

        [HttpPost("createNewReader")]
        public async Task<IActionResult> CreateNewReader([FromQuery] CreateNewReader newReader)
        {
            if (newReader == null || string.IsNullOrWhiteSpace(newReader.name) || string.IsNullOrWhiteSpace(newReader.surname))
            {
                return BadRequest("Reader data is null or invalid.");
            }

            var reader = new Readers
            {
                Name = newReader.name,
                Surname = newReader.surname,
                Birthday = newReader.Birthday,
                ContactDetails = newReader.Contact_info
            };

            await _context.Readers.AddAsync(reader);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(FindById), new { id_reader = reader.Id_Readers }, reader);
        }

        [HttpGet("getAllReaders")]
        public async Task<IActionResult> GetAllReaders()
        {
            var readers = await _context.Readers.ToListAsync();
            return Ok(new { readers, status = true });
        }

        [HttpGet("FindById/{id_reader}")]
        public async Task<IActionResult> FindById(int id_reader)
        {
            var reader = await _context.Readers.FindAsync(id_reader);
            if (reader == null)
            {
                return NotFound($"Reader with ID {id_reader} not found.");
            }

            var readerDto = new GetAllReadersId
            {
                Id_reader = reader.Id_Readers,
                name = reader.Name,
                sur_name = reader.Surname,
                Birth_year = reader.Birthday,
                Contact_Details = reader.ContactDetails
            };
            return Ok(readerDto);
        }

        [HttpPut("UpdateReader/{id_reader}")]
        public async Task<IActionResult> UpdateReader(int id_reader, [FromQuery] UpdateReaders updreader)
        {
            if (updreader == null || string.IsNullOrWhiteSpace(updreader.Name) || string.IsNullOrWhiteSpace(updreader.Surname))
            {
                return BadRequest("Update data is null or invalid.");
            }

            var reader = await _context.Readers.FindAsync(id_reader);
            if (reader == null)
            {
                return NotFound($"Reader with ID {id_reader} not found.");
            }

            reader.Name = updreader.Name;
            reader.Surname = updreader.Surname;
            reader.Birthday = updreader.Birthday;
            reader.ContactDetails = updreader.ContactDetails;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteReader/{id_reader}")]
        public async Task<IActionResult> DeleteReader(int id_reader)
        {
            var reader = await _context.Readers.FindAsync(id_reader);
            if (reader == null)
            {
                return NotFound($"Reader with ID {id_reader} not found.");
            }

            _context.Readers.Remove(reader);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}