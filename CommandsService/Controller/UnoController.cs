using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class UnoController: ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public UnoController(ICommandRepo repository,  IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UnoReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Unos from CommandsService");

            var unoItems = _repository.GetAllUnos();

            return Ok(_mapper.Map<IEnumerable<UnoReadDto>>(unoItems));
        }


        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");
            

            return Ok("Inbound test of from Uno Controller");
        }
        
    }
}