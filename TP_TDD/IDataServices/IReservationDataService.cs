using TP_TDD.Models;

namespace TP_TDD.Services;

public interface IReservationDataService
{
    Reservation? GetReservationById(int id);
    void AddReservation(Reservation reservation);
    void EndReservation(int id);
}