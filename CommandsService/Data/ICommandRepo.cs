using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {
        bool SaveChages();

        // ===== Unos =====
        IEnumerable<Uno> GetAllUnos();
        void CreateUno(Uno uno);
        bool UnoExits(int unoId);

        // ===== Commands =====
        IEnumerable<Command> GetCommandsForUno(int unoId);
        Command GetCommand(int unoId, int commandId);
        void CreateCommand(int unoId, Command command);

    }
}