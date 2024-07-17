namespace BookShare.Entities
{
    public class Reviews
    {
        public Guid Id {get; set;}
        //relacion//
        public Guid? BookId { get; set; }
        public Book? Book { get; set; }
        public Guid? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public string? Titulo {get; set;}
        public string? review {get; set;}
        public string ImagePath { get; set; }
        public string? comment {get;set;}
        public int range{get; set;}
    }
}