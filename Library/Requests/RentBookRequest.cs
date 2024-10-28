using System.ComponentModel.DataAnnotations;

namespace LessonApiBiblioteka.Requests
{
    public class RentBookRequest
    {
        [Required]
        public int Id_book { get; set; }

        [Required]
        public int Id_reader { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}