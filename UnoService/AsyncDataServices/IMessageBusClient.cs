using UnoService.Dtos;

namespace UnoService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewUno(UnoPublishedDto unoPublishedDto);
        
    }
}