namespace TP_TDD.Models;

public class Reservation
{
    public int Id { get; set; }
    public Book? Book { get; set; }
    public Member? Member { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned { get; set; } = false;
}