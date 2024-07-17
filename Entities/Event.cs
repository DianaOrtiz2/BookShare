using System.ComponentModel.DataAnnotations;

namespace BookShare.Entities
{
    public class Event
    {
        public Guid Id { get; set; }

        [Required]
        public string? Titulo { get; set; }

        [Required]
        public string? Descripcion { get; set; }

         [Required]
        public DateTime Date { get; set; }

         [Required]
        public string? Location { get; set; }
        public string ImagePath { get; set; }
    }
}