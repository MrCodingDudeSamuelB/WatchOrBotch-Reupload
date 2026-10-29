using Microsoft.EntityFrameworkCore;

public class WatchOrBotchContext(DbContextOptions<WatchOrBotchContext> options) : DbContext(options)
{
    public DbSet<WatchOrBotch.Models.Movie> Movie { get; set; } = default!;
    public DbSet<WatchOrBotch.Models.Genre> Genre { get; set; } = default!;
    public DbSet<WatchOrBotch.Models.AudienceRating> AudienceRating { get; set; } = default!;
}
