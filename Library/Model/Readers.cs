using System.ComponentModel.DataAnnotations;

namespace Library.Model
{
    public class Readers
    {
        [Key]
        public int Id_Readers { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public int Birthday { get; set; }
        public string ContactDetails { get; set; }
        //public Readers reader_id { get; internal set; }
    }
}
