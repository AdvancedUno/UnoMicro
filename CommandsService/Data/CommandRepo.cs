using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;
        public CommandRepo(AppDbContext  context)
        {
            _context = context;
        }
        public void CreateCommand(int unoId, Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            } 
            command.UnoId = unoId;
            _context.Commands.Add(command);

        }

        public void CreateUno(Uno uno)
        {
            if(uno == null)
            {
                throw new ArgumentNullException(nameof(uno));
            }
            _context.Unos.Add(uno);
            
        }

        public bool ExternalUnoExist(int externalUnoId)
        {
            return _context.Unos.Any(p => p.ExternalId == externalUnoId); 
        }

        public IEnumerable<Uno> GetAllUnos()
        {
            return _context.Unos.ToList();
        }

        public Command GetCommand(int unoId, int commandId)
        {
            return _context.Commands
                .Where(c => c.UnoId == unoId && c.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForUno(int unoId)
        {
            return _context.Commands
                .Where(c => c.UnoId == unoId)
                .OrderBy(c => c.Uno.Name);
        }

        public bool SaveChages()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool UnoExits(int unoId)
        {
            return _context.Unos.Any(p => p.Id == unoId);
        }
    }
}