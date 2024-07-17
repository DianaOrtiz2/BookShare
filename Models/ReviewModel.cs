using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShare.Models;

public class ReviewModel
{
    public Guid Id {get; set;}
    public Guid BookId { get; set; }
    public BookModel? book { get; set; }
    public List<SelectListItem> ListaLibros { get; set; }
    public Guid UserId { get; set; }

    public RegistroViewModel? user { get; set; }


    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "Titulo")]
    public string? Titulo {get; set;}
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [Display(Name = "review")]
    public string? review {get; set;}
    [Required]
    public IFormFile ImageFile { get; set; }
    public string ImagePath { get; set; }
    public string? comment {get;set;}
    public int range{get; set;}
}