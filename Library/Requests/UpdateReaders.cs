using System.ComponentModel.DataAnnotations;

namespace Library.Requests
{
    public class UpdateReaders
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int Birthday { get; set; }
        [Required]
        public string ContactDetails { get; set; }
    }
}
