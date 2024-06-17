using UnoService.Models;

namespace UnoService.Data
{
    public interface IUnoRepo
    {
        bool SaveChange();

        IEnumerable<Uno> GetAllUnos();
        Uno GetUnoById(int id);
        void CreateUno(Uno uno);

    }
}