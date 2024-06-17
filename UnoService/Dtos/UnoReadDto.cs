using System.ComponentModel.DataAnnotations;

namespace UnoService.Dtos
{
    public class UnoReadDto
    {

        public int Id {get;set;}
        
        public string Name {get;set;}

        public string Publisher {get;set;}

        public string  Cost {get;set;}
    }
}