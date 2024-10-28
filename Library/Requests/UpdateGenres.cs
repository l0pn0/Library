using System.ComponentModel.DataAnnotations;

namespace Library.Requests
{
    public class UpdateGenres
    {
        [Required]
        public string Name_genre { get; set; }
    }
}
