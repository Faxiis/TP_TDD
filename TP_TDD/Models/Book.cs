namespace TP_TDD.Models;

public class Book
{
        public required string Isbn { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Publisher { get; set; }
        public required string Format { get; set; }
        public required bool IsAvailable { get; set; } 
}
