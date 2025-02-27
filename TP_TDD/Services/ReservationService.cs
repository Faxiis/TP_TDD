using TP_TDD.Models;

namespace TP_TDD.Services;

public class ReservationService(IReservationDataService databaseService)
{

    public void AddReservation(Reservation reservation)
    {
        databaseService.AddReservation(reservation);
    }
    
}
