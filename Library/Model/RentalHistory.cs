using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Model;

namespace Library.Model
{
    public class RentalHistory
    {
        [Key]
        public int Id_RentalHistory { get; set; }
        [Required]
        public DateTime rental_date { get; set; }
        public DateTime? return_date { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public Book Book { get; set; }
        public Readers Reader { get; set; }


        [Required]
        [ForeignKey("Book")]
        public int book_id { get; set; }
        [Required]
        [ForeignKey("Reader")]
        public int reader_id { get; set; }



        //__tablename__ = 'rentals'
        //id = Column(Integer, primary_key= True)
        //book_id = Column(Integer, ForeignKey('books.id'))
        //reader_id = Column(Integer, ForeignKey('readers.id'))
        //rental_date = Column(Date)
        //return_date = Column(Date)

        //book = relationship('Book')
        //reader = relationship('Reader')
    }
}
