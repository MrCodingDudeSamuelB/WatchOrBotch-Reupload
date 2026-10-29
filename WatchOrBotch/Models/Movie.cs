using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WatchOrBotch.Models
{
    public class Movie
    {

        public int Id { get; set; }
        public int GenreId { get; set; }
        public int AudienceRatingId { get; set; }
        // navigation property
        [ValidateNever]
        public Genre Genre { get; set; } = null!;
        [ValidateNever]
        public AudienceRating AudienceRating { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;

        [Display(Name = "Runtime (min)")]
        public double Runtime { get; set; }
        //public double Rating { get; set; }
        //public string Genre { get; set; } = string.Empty;
        
        [Display(Name = "Release Date")]
        public string Releasedate { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}