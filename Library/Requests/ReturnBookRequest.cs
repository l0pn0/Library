using System.ComponentModel.DataAnnotations;

namespace Library.Requests
{
    public class ReturnBookRequest
    {
        [Required]
        public int RentalId { get; set; }
    }
}