
using System.ComponentModel.Design;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/uno/{unoId}/[controller]")]
    [ApiController]
    public class CommandsController: ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommansForUno(int unoId)
        {
            Console.WriteLine($"--> Hit GetCommandsForUno: {unoId}");

            if(!_repository.UnoExits(unoId))
            {
                return NotFound();
            }

            var commands = _repository.GetCommandsForUno(unoId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForUno")]
        public ActionResult<CommandReadDto> GetCommandForUno(int unoId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForUno: {unoId} / {commandId}");

            if(!_repository.UnoExits(unoId))
            {
                return NotFound();
            }

            var command = _repository.GetCommand(unoId, commandId);

            if(command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));
        }
        
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForUno(int unoId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"--> Hit CreateCommandForUno: {unoId}");

            if(!_repository.UnoExits(unoId))
            {
                Console.WriteLine("--> Cannot find Uno object");
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDto);

            _repository.CreateCommand(unoId, command);
            _repository.SaveChages();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForUno),
                new {unoId = unoId, commandId = commandReadDto.Id}, commandReadDto);
        }
    }
}