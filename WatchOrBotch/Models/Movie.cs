using System.ComponentModel.DataAnnotations;

namespace WatchOrBotch.Models
{
    public class Movie
    {

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;

        [Display(Name = "Runtime(min)")]
        public double Runtime { get; set; }
        public double Rating { get; set; }
        public string Genre { get; set; } = string.Empty;
        
        [Display(Name = "Runtime(min)")]
        public string Releasedate { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}