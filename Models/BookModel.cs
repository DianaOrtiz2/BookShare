using System.ComponentModel.DataAnnotations;

namespace BookShare.Models;

public class BookModel
{
    public Guid Id {get; set;}
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Titulo")]
    public string? Titulo {get; set;}
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Autor")]
    public string? Autor {get;set;}

    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Descripcion")]
    public string? Descripcion  {get;set;}
    [Required]
    public IFormFile ImageFile { get; set; }

     [Required]
    public IFormFile DocumentFile { get; set; }
    public string? ImagePath { get; set; }
    public string? DocumentPath { get; set; } 
}