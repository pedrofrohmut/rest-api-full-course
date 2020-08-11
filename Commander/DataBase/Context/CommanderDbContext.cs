using Microsoft.EntityFrameworkCore;
using Commander.Models;

namespace Commander.DataBase.Context
{
  public class CommanderDbContext : DbContext
  {
    public CommanderDbContext(DbContextOptions<CommanderDbContext> options) : base(options) { }

    public DbSet<Command> Commands { get; set; }
  }
}
