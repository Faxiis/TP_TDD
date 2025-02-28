using TP_TDD.Models;

namespace TP_TDD.Services;

public class ReservationService(IReservationDataService databaseService, IEmailService emailService)
{

    public Reservation? GetReservationById(int id)
    {
        return databaseService.GetReservationById(id);
    }
    
    public void AddReservation(Reservation reservation)
    {
        if (reservation.Book == null || reservation.Member == null)
        {
            throw new ArgumentException("La réservation doit avoir un membre et un livre.");
        }
            
        if (reservation.ReturnDate > reservation.ReservationDate.AddMonths(4) || Has3Reservations(reservation.Member))
        {
            return;
        }
        
        databaseService.AddReservation(reservation);
    }
    
    public void EndReservation(int id)
    {
        var reservation = databaseService.GetReservationById(id);
        
        if (reservation != null)
        {
            reservation.IsReturned = true;
        }
        else
        {
            throw new ArgumentException("La réservation n'existe pas.");
        }
    }
    
    private bool Has3Reservations(Member member)
    {
        var nbReservations = databaseService.GetReservationByMember(member.Code);
        
        return nbReservations?.Count(r => !r.IsReturned) >= 3;
    }
    
    public List<Reservation>? GetReservations()
    {
        return databaseService.GetReservations()?.Where(r => r.IsReturned == false).ToList();
    }
    
    public List<Reservation>? GetReservationsByMember(int code)
    {
        return databaseService.GetReservationByMember(code)?.ToList();
    }
    
    
    public void SendOverdueReservationReminders()
    {
        var overdueReservations = databaseService.GetOverdueReservations();
        var memberReservations = overdueReservations.GroupBy(r => r.Member);

        foreach (var memberGroup in memberReservations)
        {
            var member = memberGroup.Key;
            var reservations = memberGroup.ToList();
            emailService.SendReminderEmail(member, reservations);
        }
    }
}
