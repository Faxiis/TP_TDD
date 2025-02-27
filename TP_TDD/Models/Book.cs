namespace TP_TDD.Models;

public class Book
{
        public required string Isbn { get; set; }
        public string? Title { get; set; }
        public Author? Author { get; set; }
        public Publisher? Publisher { get; set; }
        public string? Format { get; set; }
        public bool IsAvailable { get; set; } = true;
}
