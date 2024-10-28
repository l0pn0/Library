using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Model
{
    public class Genres
    {
        [Key]
        public int Id_Genres { get; set; }
        public string Title { get; set; }


    }
}
