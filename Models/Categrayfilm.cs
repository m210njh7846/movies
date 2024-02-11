using System.ComponentModel.DataAnnotations.Schema;

namespace books.Models
{
    public class Categrayfilm
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string descrption { get; set; }
        [NotMapped]
        public required IFormFile File { get; set; }

        public string tittle { get; set; }
        public List<Film> Film { get; set; }

    }
}
