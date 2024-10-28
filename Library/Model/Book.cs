using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Model
{
    public class Book
    {
        [Key]
        public int Id_Book { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int YearOfPublication { get; set; }
        public int AvailableCopies { get; set; }

        [ForeignKey("Genres")]
        public int Genre_Id { get; set; }
        public Genres Genres { get; set; }

    }
}
