using System.ComponentModel.DataAnnotations;

namespace Library.Requests
{
    public class UpdateBooks
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Year_public { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Copies { get; set; }
        [Required]
        public int Id_genre { get; set; }
    }
}
