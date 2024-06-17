using System.ComponentModel.DataAnnotations;

namespace UnoService.Dtos
{
    public class UnoCreateDto
    {
        [Required]
        public string Name {get;set;}

        [Required]
        public string Publisher {get;set;}

        [Required]
        public string  Cost {get;set;}
    }
}