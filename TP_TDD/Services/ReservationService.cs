using TP_TDD.Models;

namespace TP_TDD.Services;

public class ReservationService(IReservationDataService databaseService)
{

    public void AddReservation(Reservation reservation)
    {
        if (reservation.ReturnDate > reservation.ReservationDate.AddMonths(4))
        {
            return;
        }
        
        databaseService.AddReservation(reservation);
    }
    
    public Reservation? GetReservationById(int id)
    {
        return databaseService.GetReservationById(id);
    }
    
}
