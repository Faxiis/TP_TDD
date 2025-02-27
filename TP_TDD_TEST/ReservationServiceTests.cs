using Moq;
using TP_TDD.Models;
using TP_TDD.Services;

namespace TP_TDD_TEST;

public class ReservationServiceTests
{
    private Mock<IReservationDataService> _mockDatabaseService;

    [SetUp]
    public void Setup()
    {
        _mockDatabaseService = new Mock<IReservationDataService>();
    }
    
    [Test]
    public void GetReservationById_ShouldReturnReservation()
    {
        var author = new Author() { Id = 1, LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        var member = new Member() { Code = 1, LastName = "Test Member", FirstName = "Test Member", BirthDate = new DateTime(2000, 1, 1), Civility = "M" };
        var expectedReservation = new Reservation() { Id = 1, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };

        _mockDatabaseService.Setup(service => service.GetReservationById(1)).Returns(expectedReservation);

        var reservationService = new ReservationService(_mockDatabaseService.Object);

        var reservation = reservationService.GetReservationById(1);
        Assert.That(reservation, Is.EqualTo(expectedReservation));
    }
    
    [Test]
    public void AddReservation_ShouldAddReservationToLibrary()
    {
        var author = new Author() { Id = 1, LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        var member = new Member() { Code = 1, LastName = "Test Member", FirstName = "Test Member", BirthDate = new DateTime(2000, 1, 1), Civility = "M" };
        var reservation = new Reservation() { Id = 1, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };

        _mockDatabaseService.Setup(service => service.GetReservationById(1)).Returns(reservation);
        
        var reservationService = new ReservationService(_mockDatabaseService.Object);
        reservationService.AddReservation(reservation);

        _mockDatabaseService.Verify(service => service.AddReservation(It.IsAny<Reservation>()), Times.Once);;
        Assert.IsNotNull(reservationService.GetReservationById(1));
        Assert.That(reservationService.GetReservationById(1), Is.EqualTo(reservation));
    }
    
    [Test]
    public void AddReservation_ShouldNotAddReservationToLibrary_TimeIsSuperiorToForMonth()
    {
        var author = new Author() { Id = 1, LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        var member = new Member() { Code = 1, LastName = "Test Member", FirstName = "Test Member", BirthDate = new DateTime(2000, 1, 1), Civility = "M" };
        var reservation = new Reservation() { Id = 1, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 8, 24) };

        var reservationService = new ReservationService(_mockDatabaseService.Object);
        reservationService.AddReservation(reservation);

        _mockDatabaseService.Verify(service => service.AddReservation(It.IsAny<Reservation>()), Times.Never);;
        Assert.IsNull(reservationService.GetReservationById(1));
    }
    
    [Test]
    public void EndReservation_ShouldEndReservation()
    {
        var author = new Author() { Id = 1, LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        var member = new Member() { Code = 1, LastName = "Test Member", FirstName = "Test Member", BirthDate = new DateTime(2000, 1, 1), Civility = "M" };
        var reservation = new Reservation() { Id = 1, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };

        _mockDatabaseService.Setup(service => service.GetReservationById(1)).Returns(reservation);
        
        var reservationService = new ReservationService(_mockDatabaseService.Object);
        reservationService.EndReservation(1);

        Assert.IsTrue(reservation.IsReturned);
    }
    
    // test qu'un adhérent ne peut pas avoir plus de 3 réservations en cours
    [Test]
    public void AddReservation_ShouldNotAddReservationToLibrary_MemberHas3Reservations()
    {
        var author = new Author() { Id = 1, LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        var member = new Member() { Code = 1, LastName = "Test Member", FirstName = "Test Member", BirthDate = new DateTime(2000, 1, 1), Civility = "M" };
        var reservation1 = new Reservation() { Id = 1, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };
        var reservation2 = new Reservation() { Id = 2, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };
        var reservation3 = new Reservation() { Id = 3, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };
        var reservation = new Reservation() { Id = 4, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };

        _mockDatabaseService.Setup(service => service.GetReservationById(1)).Returns(reservation1);
        _mockDatabaseService.Setup(service => service.GetReservationById(2)).Returns(reservation2);
        _mockDatabaseService.Setup(service => service.GetReservationById(3)).Returns(reservation3);
        
        var reservationService = new ReservationService(_mockDatabaseService.Object);
        reservationService.AddReservation(reservation);

        _mockDatabaseService.Verify(service => service.AddReservation(It.IsAny<Reservation>()), Times.Never);;
        Assert.IsNull(reservationService.GetReservationById(4));
    }
}
