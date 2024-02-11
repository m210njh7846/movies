using System.ComponentModel.DataAnnotations.Schema;

namespace books.Models
{
    public class Film
    {
        public int  Id { get; set; }
        public int categrayfilmid { get; set; }
        public string name { get; set; }
        public Categrayfilm categrayfilm { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        [NotMapped]
        public required IFormFile File { get; set; }


    }
}
