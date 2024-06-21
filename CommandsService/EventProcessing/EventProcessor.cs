using System.Text.Json;
using AutoMapper;
using CommandsService.Dtos;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactor;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactor, IMapper mapper)
        {
            _scopeFactor = scopeFactor;
            _mapper = mapper;
            
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch(eventType)
            {
                case EventType.UnoPublished:
                    // Todo
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage){
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            
            switch(eventType.Event)
            {
                case "Uno_Published":
                    Console.WriteLine("--> Uno Published Event Detected");
                    return EventType.UnoPublished;
                default:
                    Console.WriteLine("--> Could Not Determine the Event Type");
                    return EventType.Undetermined;
            }
        }
    }

    enum EventType
    {
        UnoPublished,
        Undetermined
    }
}