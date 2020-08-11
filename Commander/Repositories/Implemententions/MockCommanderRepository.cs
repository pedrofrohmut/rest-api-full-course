using System.Collections.Generic;
using Commander.Models;

namespace Commander.Repositories.Implementations
{
  public class MockCommanderRepository : ICommanderRepository
  {
    public void CreateCommand(Command newCommand)
    {
      throw new System.NotImplementedException();
    }

    public void DeleteCommand(Command commandToDelete)
    {
      throw new System.NotImplementedException();
    }
    public IEnumerable<Command> GetAllCommands()
    {
      var commands = new List<Command>
      {
        new Command
        {
          Id = 0,
          HowTo = "Boil an Egg",
          Line = "boil water",
          Platform = "Kettle & Pan"
        },
        new Command
        {
          Id = 1,
          HowTo = "Cut Bread",
          Line = "get a knife",
          Platform = "Kettle & Knife"
        },
        new Command
        {
          Id = 2,
          HowTo = "Make cup of tea",
          Line = "Place teabag in cup",
          Platform = "Kettle & Cup"
        }
      };
      return commands;
    }

    public Command GetCommandById(int id)
    {
      return new Command
      {
        Id = 0,
        HowTo = "Boil an Egg",
        Line = "boil water",
        Platform = "Kettle & Pan"
      };
    }

    public bool SaveChanges()
    {
      throw new System.NotImplementedException();
    }

    public void UpdateCommand(Command updatedCommand)
    {
      throw new System.NotImplementedException();
    }
  }
}
