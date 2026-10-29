using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WatchOrBotch.Models
{
    public class AudienceRating
    {
        public int Id { get; set; }

        [ValidateNever]
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public string Title { get; set; } = string.Empty;
    }
}
