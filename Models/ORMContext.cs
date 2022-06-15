#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace ORM.Models;
public class ORMContext : DatabaseContext
{
    public ORMContext(DatabaseContextOptions options) : base (options) {  }
    public DbSet<Users> {get; set; }
}