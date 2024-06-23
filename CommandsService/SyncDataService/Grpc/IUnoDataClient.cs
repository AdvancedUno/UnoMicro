using CommandsService.Models;

namespace CommandsService.SyncDataService.Grpc
{
    public interface IUnoDataClient
    {
        IEnumerable<Uno> ReturnAllUno();
        
    }
}