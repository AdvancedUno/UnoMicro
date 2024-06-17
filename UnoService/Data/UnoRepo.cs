using UnoService.Models;

namespace UnoService.Data
{
    public class UnoRepo : IUnoRepo
    {
        private readonly AppDbContext _context;

        public UnoRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateUno(Uno uno)
        {
            if(uno == null){
                throw new ArgumentNullException(nameof(uno));
            }

            _context.Unos.Add(uno);
        }

        public IEnumerable<Uno> GetAllUnos()
        {
            return _context.Unos.ToList();
        }

        public Uno GetUnoById(int id)
        {
            return _context.Unos.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChange()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}