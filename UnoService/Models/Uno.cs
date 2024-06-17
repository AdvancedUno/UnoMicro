using System.ComponentModel.DataAnnotations;

namespace UnoService.Models
{
    public class Uno
    {
        [Key]
        public required int Id {get;set;}
        
        public required string Name {get;set;}

        public required string Publisher {get;set;}

        public required string  Cost {get;set;}
    }
}