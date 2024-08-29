using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models
{
    public class GenreModel
    {
        public List<Movie> Movies { get; set;}
        public SelectList? Genres { get; set;}
        public string? MovieGenre {  get; set;}
        public string Search { get; set;}
    }
}
