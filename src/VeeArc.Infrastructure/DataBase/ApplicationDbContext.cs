using Microsoft.EntityFrameworkCore;
using VeeArc.Domain.Entities;
using VeeArc.Infrastructure.Common.Interfaces;

namespace VeeArc.Infrastructure.DataBase;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Article> Articles { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public async Task SaveAsync()
    {
        await SaveChangesAsync();
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .HasOne(article => article.User)
            .WithMany(user => user.Articles);

        modelBuilder.Entity<Role>()
            .HasMany(role => role.Users)
            .WithMany(user => user.Roles);
    }
}
