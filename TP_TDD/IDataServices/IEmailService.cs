using TP_TDD.Models;

namespace TP_TDD.Services;

public interface IEmailService
{
    void SendReminderEmail(Member member, List<Reservation> reservations);
}