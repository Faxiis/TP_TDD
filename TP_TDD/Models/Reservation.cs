namespace TP_TDD.Models;

public class Reservation
{
    public int Id { get; set; }
    public required Book Book { get; set; }
    public required Member Member { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned { get; set; } = false;
}