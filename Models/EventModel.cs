using System.ComponentModel.DataAnnotations;

namespace BookShare.Models;

public class EventModel
{
    public Guid Id { get; set; }

        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Titulo")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripcion")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Location")]
        public string? Location { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
        public string ImagePath { get; set; }
}