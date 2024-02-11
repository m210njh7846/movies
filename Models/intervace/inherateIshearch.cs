
using Microsoft.EntityFrameworkCore;

namespace books.Models.intervace
{
    public class inherateIshearch : Isearch<Film>
    {
        private moviesDbcontext dbcontext;

        public inherateIshearch(moviesDbcontext db)
        {
            dbcontext = db;
        }
        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Film> Search(string term)
        {
            var result = dbcontext.film.Include(a => a.categrayfilm).Where(b => b.name.Contains(term)).ToList();
            return result;
        }
    }
}
