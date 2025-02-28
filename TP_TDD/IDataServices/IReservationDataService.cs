using TP_TDD.Models;

namespace TP_TDD.Services;

public interface IReservationDataService
{
    Reservation? GetReservationById(int id);
    List<Reservation>? GetReservationByMember(int code);
    List<Reservation>? GetReservations();
    List<Reservation> GetOverdueReservations();
    void AddReservation(Reservation reservation);
    void EndReservation(int id);
}