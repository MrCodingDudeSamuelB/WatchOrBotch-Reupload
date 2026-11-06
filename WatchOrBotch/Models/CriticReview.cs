using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WatchOrBotch.Models
{
    public class CriticReview
    {
        // primary key
        public int Id { get; set; }

        // foreign key
        [Display(Name = "Critic")]
        public int MovieId { get; set; }

        // navigation property
        [ValidateNever]
        [ScaffoldColumn(false)]
        public Movie Movie { get; set; } = null!;



        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(1, 5)]
        [Display(Name = "Rating (1-5 stars)")]
        public int Rating { get; set; } // 1-5 stars

        [Required]
        public bool IsPublished { get; set; } = false;

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = string.Empty; // store the auth0 userId

        [ScaffoldColumn(false)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
