using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
namespace BookShare.Entities
{
    public class Book
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
}