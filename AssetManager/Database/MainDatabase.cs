using AssetManager.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Database;

public class MainDatabase : DbContext
{
    public DbSet<File> Files { get; private set; }

    public DbSet<User> Users { get; private set; }

    public MainDatabase(DbContextOptions<MainDatabase> options) : base(options)
    {
    }
}