using System.ComponentModel.DataAnnotations;

namespace Library.Requests
{
    public class CreateNewGenre
    {
        [Required]
        public string Name_genre { get; set; }
    }
}
