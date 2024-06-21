using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UnoService.AsyncDataServices;
using UnoService.Data;
using UnoService.Dtos;
using UnoService.Models;
using UnoService.SyncDataService.HttpContext;

namespace UnoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnoController: ControllerBase
    {   
        private readonly IUnoRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public UnoController(
            IUnoRepo repository, 
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient
            )
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UnoReadDto>> GetUnos()
        {
            Console.WriteLine("--> Getting Platforms...");

            var unoItem = _repository.GetAllUnos();

            return Ok(_mapper.Map<IEnumerable<UnoReadDto>>(unoItem));
        }

        [HttpGet("{id}", Name = "GetUnoById")]
        public ActionResult<UnoReadDto> GetUnoById(int id)
        {
            var unoItem = _repository.GetUnoById(id);
            if(unoItem != null){
                return Ok(_mapper.Map<UnoReadDto>(unoItem));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UnoReadDto>> CreateUno(UnoCreateDto unoCreateDto)
        {
            var unoModel = _mapper.Map<Uno>(unoCreateDto);

            _repository.CreateUno(unoModel);
            _repository.SaveChange();

            var unoReadDto = _mapper.Map<UnoReadDto>(unoModel);

            // Send Sync Message
            try
            {
                await _commandDataClient.SendUnotToCommand(unoReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously : {ex.Message}");
            }

            //Send Async Message
            try
            {
                var unoPublishedDto = _mapper.Map<UnoPublishedDto>(unoReadDto);
                unoPublishedDto.Event = "Uno_Published";
                _messageBusClient.PublishNewUno(unoPublishedDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously : {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetUnoById), new {Id = unoReadDto.Id}, unoReadDto);
        }


    }
}