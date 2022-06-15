#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace LoginAndRegistration.Models;
public class ORMContext : DbContext
{
    public ORMContext(DbContextOptions options) : base (options) {  }
    public DbSet<User> Users { get; set; }
}