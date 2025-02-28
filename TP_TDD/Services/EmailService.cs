using TP_TDD.Models;

namespace TP_TDD.Services;

public class EmailService : IEmailService
{
    public void SendReminderEmail(Member member, List<Reservation> reservations)
    {
        Console.WriteLine($"Sending email to {member.FirstName} {member.LastName} with {reservations.Count} overdue reservations.");
    }
}
