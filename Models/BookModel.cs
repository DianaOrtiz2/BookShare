using System.ComponentModel.DataAnnotations;

namespace BookShare.Models;

public class BookModel
{
    public Guid Id {get; set;}
    [Required]
    public string? Titulo {get; set;}
    [Required]
    public string? Autor {get;set;}

    [Required]
    public string? Descripcion  {get;set;}
    [Required]
    public byte[]? Image {get; set;}
}