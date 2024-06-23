using AutoMapper;
using Grpc.Core;
using UnoService.Data;

namespace UnoService.SyncDataService.Grpc
{
    public class GrpcUnoService : GrpcUno.GrpcUnoBase
    {
        private readonly IUnoRepo _repository;
        private readonly IMapper _mapper;

        public GrpcUnoService(IUnoRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<UnoResponse> GetAllUno(GetAllRequest request, ServerCallContext context)
        {
            var response = new UnoResponse();
            var unos = _repository.GetAllUnos();

            foreach(var uno in unos)
            {
                response.Uno.Add(_mapper.Map<GrpcUnoModel>(uno));
            }
            
            return Task.FromResult(response);
        }
    }
}