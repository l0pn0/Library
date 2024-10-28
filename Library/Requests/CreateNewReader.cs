using System.ComponentModel.DataAnnotations;

namespace Library.Requests
{
    public class CreateNewReader
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string surname { get; set; }
        [Required]
        public int Birthday { get; set; }
        [Required]
        public string Contact_info { get; set; }
    }
}
