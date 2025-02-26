using TP_TDD.Models;

namespace TP_TDD.Data;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    
    public DbSet<Book> Books { get; set; }
}
