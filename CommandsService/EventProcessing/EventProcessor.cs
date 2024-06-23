using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch(eventType)
            {
                case EventType.UnoPublished:
                    addUno(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
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

        private void addUno(string unoPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

                var unoPublishedDto = JsonSerializer.Deserialize<UnoPublishedDto>(unoPublishedMessage);

                try
                {
                    var uno = _mapper.Map<Uno>(unoPublishedDto);

                    if(!repo.ExternalUnoExist(uno.ExternalId))
                    {
                        repo.CreateUno(uno);
                        repo.SaveChages();
                        Console.WriteLine("--> Uno object added!");
                    }
                    else
                    {
                        Console.WriteLine("--> Uno object is already exists...");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not add Uno object to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        UnoPublished,
        Undetermined
    }
}