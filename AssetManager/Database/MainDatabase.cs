using AssetManager.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Database;

public class MainDatabase : DbContext
{
    public DbSet<File> Files { get; }

    public DbSet<User> Users { get; }

    public MainDatabase(DbContextOptions<MainDatabase> options) : base(options)
    {
    }
}