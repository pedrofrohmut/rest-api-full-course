using System.Collections.Generic;
using AutoMapper;
using Commander.Dtos;
using Commander.Models;
using Commander.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
  [Route("api/commands")]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly ICommanderRepository repo;
    private readonly IMapper mapper;

    public CommandsController(ICommanderRepository repo, IMapper mapper)
    {
      this.repo = repo;
      this.mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
    {
      var commands = this.repo.GetAllCommands();
      return Ok(this.mapper.Map<IEnumerable<CommandReadDto>>(commands));
    }

    [HttpGet("{id}", Name = "GetCommandById")]
    public ActionResult<CommandReadDto> GetCommandById([FromRoute] int id)
    {
      var command = this.repo.GetCommandById(id);
      if (command == null)
        return NotFound();
      return Ok(this.mapper.Map<CommandReadDto>(command));
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand([FromBody] CommandCreateDto requestBody)
    {
      var command = this.mapper.Map<Command>(requestBody);
      this.repo.CreateCommand(command);
      this.repo.SaveChanges();
      var commandReadDto = this.mapper.Map<CommandReadDto>(command);
      return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCommand([FromRoute] int id, [FromBody] CommandUpdateDto requestBody)
    {
      var commandRepo = this.repo.GetCommandById(id);
      if (commandRepo == null)
        return NotFound();
      // Model updated with the new info from requestBody
      this.mapper.Map(requestBody, commandRepo);
      this.repo.UpdateCommand(commandRepo);
      this.repo.SaveChanges();
      return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult PartialUpdateCommand(
      [FromRoute] int id,
      [FromBody] JsonPatchDocument<CommandUpdateDto> requestBody)
    {
      var commandRepo = this.repo.GetCommandById(id);
      if (commandRepo == null)
        return NotFound();
      var commandToPatch = this.mapper.Map<CommandUpdateDto>(commandRepo);
      requestBody.ApplyTo(commandToPatch, ModelState);
      if (!TryValidateModel(commandToPatch))
        return ValidationProblem();
      this.mapper.Map(commandToPatch, commandRepo);
      this.repo.SaveChanges();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCommand([FromRoute] int id)
    {
      var commandRepo = this.repo.GetCommandById(id);
      if (commandRepo == null) 
        return NotFound();
      this.repo.DeleteCommand(commandRepo);
      this.repo.SaveChanges();
      return NoContent();
    }
  }
}
