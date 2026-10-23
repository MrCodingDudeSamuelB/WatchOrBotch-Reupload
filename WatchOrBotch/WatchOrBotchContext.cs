using Microsoft.EntityFrameworkCore;

public class WatchOrBotchContext(DbContextOptions<WatchOrBotchContext> options) : DbContext(options)
{
    public DbSet<WatchOrBotch.Models.Movie> Movie { get; set; } = default!;
}
