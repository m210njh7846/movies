using Microsoft.EntityFrameworkCore;

namespace books.Models
{
    
        public class moviesDbcontext : DbContext
        {
            public moviesDbcontext(DbContextOptions<moviesDbcontext> option) : base(option)
            {

            }

            public DbSet<Categrayfilm> categrayfilm { get; set; }
            public DbSet<Contact> Contact { get; set; }
            public DbSet<Film> film { get; set; }

        internal object Find(string term)
        {
            throw new NotImplementedException();
        }
    }
    
}
