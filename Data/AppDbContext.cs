using Task.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Task.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }
    public DbSet<User> Users { get; set; } = null!;
}

