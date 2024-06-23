using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using UnoService;

namespace CommandsService.SyncDataService.Grpc
{
    public class UnoDataClient : IUnoDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UnoDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public IEnumerable<Uno> ReturnAllUno()
        {
            Console.WriteLine($"--> Callaing Grpc Service {_configuration["GrpcUno"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcUno"]);
            var client = new GrpcUno.GrpcUnoClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllUno(request);
                return _mapper.Map<IEnumerable<Uno>>(reply.Uno);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Cound not call Grpc Server {ex.Message}");
                return null;
            }
            
        }
    }
}