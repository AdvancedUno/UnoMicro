using UnoService.Dtos;

namespace UnoService.SyncDataService.HttpContext{
    public interface ICommandDataClient
    {
        Task SendUnotToCommand(UnoReadDto uno);

    }
}