using Microsoft.EntityFrameworkCore;

public class WatchOrBotchContext(DbContextOptions<WatchOrBotchContext> options) : DbContext(options)
{
    public DbSet<WatchOrBotch.Models.Movie> Movie { get; set; } = default!;
    public DbSet<WatchOrBotch.Models.Genre> Genre { get; set; } = default!;
    public DbSet<WatchOrBotch.Models.AudienceRating> AudienceRating { get; set; } = default!;

    public DbSet<WatchOrBotch.Models.CriticReview> CriticReview { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WatchOrBotch.Models.CriticReview>()
            .HasIndex(review => new
            {
                review.CreatedBy,
                review.MovieId
            })
            .IsUnique();
    }
}
