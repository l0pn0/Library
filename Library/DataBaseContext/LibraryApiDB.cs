using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.DataBaseContext
{
    public class LibraryApiDB : DbContext
    {
        public LibraryApiDB(DbContextOptions options) : base(options)
        { }

                public DbSet<Book> Book { get; set; }
                public DbSet<Genres> Genres { get; set; }
                public DbSet<Readers> Readers { get; set; }
                public DbSet<RentalHistory> RentalHistory { get; set; }

    }    
}
