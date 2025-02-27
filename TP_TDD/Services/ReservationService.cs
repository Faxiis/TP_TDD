using TP_TDD.Models;

namespace TP_TDD.Services;

public class ReservationService(IReservationDataService databaseService)
{

    public Reservation? GetReservationById(int id)
    {
        return databaseService.GetReservationById(id);
    }
    
    public void AddReservation(Reservation reservation)
    {
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
    }
    
    private bool Has3Reservations(Member member)
    {
        var nbReservations = databaseService.GetReservationByMember(member.Code);
        
        return nbReservations?.Count(r => !r.IsReturned) >= 3;
    }
}
