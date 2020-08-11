using System;
using System.Collections.Generic;
using System.Linq;
using Commander.DataBase.Context;
using Commander.Models;

namespace Commander.Repositories.Implementations
{
  public class PostgreSQLCommanderRepository : ICommanderRepository
  {
    private readonly CommanderDbContext context;

    public PostgreSQLCommanderRepository(CommanderDbContext context)
    {
      this.context = context;
    }

    public void CreateCommand(Command newCommand)
    {
      if (newCommand == null)
        throw new ArgumentNullException(nameof(newCommand));
      this.context.Add(newCommand);
    }

    public void DeleteCommand(Command commandToDelete)
    {
      if (commandToDelete == null)
        throw new ArgumentNullException(nameof(commandToDelete));
      this.context.Commands.Remove(commandToDelete);
    }

    public IEnumerable<Command> GetAllCommands() =>
      this.context.Commands.ToList();

    public Command GetCommandById(int id) =>
      this.context.Commands.FirstOrDefault(cmd => cmd.Id == id);

    public bool SaveChanges() =>
      this.context.SaveChanges() >= 0;

    public void UpdateCommand(Command updatedCommand) { }
  }
}
