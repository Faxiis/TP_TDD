using Moq;
using TP_TDD.Models;
using TP_TDD.Services;

namespace TP_TDD_TEST;

public class ReservationServiceTests
{
    private Mock<IReservationDataService> _mockDatabaseService;
    private Mock<IEmailService> _mockEmailService;

    [SetUp]
    public void Setup()
    {
        _mockDatabaseService = new Mock<IReservationDataService>();
        _mockEmailService = new Mock<IEmailService>();
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

        var reservationService = new ReservationService(_mockDatabaseService.Object, _mockEmailService.Object);

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
        
        var reservationService = new ReservationService(_mockDatabaseService.Object, _mockEmailService.Object);
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

        var reservationService = new ReservationService(_mockDatabaseService.Object, _mockEmailService.Object);
        reservationService.AddReservation(reservation);

        _mockDatabaseService.Verify(service => service.AddReservation(It.IsAny<Reservation>()), Times.Never);;
        Assert.IsNull(reservationService.GetReservationById(1));
    }
    
    [Test]
    public void AddReservation_ShouldNotAddReservationToLibrary_WhenBookIsNull()
    {
        var member = new Member() { Code = 1, LastName = "Test Member", FirstName = "Test Member", BirthDate = new DateTime(2000, 1, 1), Civility = "M" };
        var reservation = new Reservation() { Id = 1, Book = null, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };

        var reservationService = new ReservationService(_mockDatabaseService.Object, _mockEmailService.Object);
        reservationService.AddReservation(reservation);

        _mockDatabaseService.Verify(service => service.AddReservation(It.IsAny<Reservation>()), Times.Never);
        Assert.IsNull(reservationService.GetReservationById(1));
    }

    [Test]
    public void AddReservation_ShouldNotAddReservationToLibrary_WhenMemberIsNull()
    {
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = new Author() { Id = 1, LastName = "Test Author", FirstName = "Test Author" }, Publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" }, Format = "Poche", IsAvailable = true };
        var reservation = new Reservation() { Id = 1, Book = book, Member = null, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };

        var reservationService = new ReservationService(_mockDatabaseService.Object, _mockEmailService.Object);
        reservationService.AddReservation(reservation);

        _mockDatabaseService.Verify(service => service.AddReservation(It.IsAny<Reservation>()), Times.Never);
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
        
        var reservationService = new ReservationService(_mockDatabaseService.Object, _mockEmailService.Object);
        reservationService.EndReservation(1);

        Assert.IsTrue(reservation.IsReturned);
    }
    
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

        _mockDatabaseService.Setup(service => service.GetReservationByMember(1)).Returns(new List<Reservation> { reservation1, reservation2, reservation3 });

        var reservationService = new ReservationService(_mockDatabaseService.Object, _mockEmailService.Object);
        reservationService.AddReservation(reservation);

        _mockDatabaseService.Verify(service => service.AddReservation(It.IsAny<Reservation>()), Times.Never);
        Assert.IsNull(reservationService.GetReservationById(4));
    }
    
    [Test]
    public void GetReservation_ShouldReturnReservations()
    {
        var author = new Author() { Id = 1, LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        var member = new Member() { Code = 1, LastName = "Test Member", FirstName = "Test Member", BirthDate = new DateTime(2000, 1, 1), Civility = "M" };
        var reservation1 = new Reservation() { Id = 1, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };
        var reservation2 = new Reservation() { Id = 2, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };
        var reservation3 = new Reservation() { Id = 3, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };

        _mockDatabaseService.Setup(service => service.GetReservations()).Returns(new List<Reservation> { reservation1, reservation2, reservation3 });

        var reservationService = new ReservationService(_mockDatabaseService.Object, _mockEmailService.Object);
        var reservations = reservationService.GetReservations();

        Assert.That(reservations, Is.EqualTo(new List<Reservation> { reservation1, reservation2, reservation3 }));
    }
    
    [Test]
    public void GetReservationByMember_ShouldReturnReservations()
    {
        var author = new Author() { Id = 1, LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        var member = new Member() { Code = 1, LastName = "Test Member", FirstName = "Test Member", BirthDate = new DateTime(2000, 1, 1), Civility = "M" };
        var reservation1 = new Reservation() { Id = 1, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };
        var reservation2 = new Reservation() { Id = 2, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };
        var reservation3 = new Reservation() { Id = 3, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 3, 24) };

        _mockDatabaseService.Setup(service => service.GetReservationByMember(1)).Returns(new List<Reservation> { reservation1, reservation2, reservation3 });

        var reservationService = new ReservationService(_mockDatabaseService.Object, _mockEmailService.Object);
        var reservations = reservationService.GetReservationsByMember(1);

        Assert.That(reservations, Is.EqualTo(new List<Reservation> { reservation1, reservation2, reservation3 }));
    }
    
    [Test]
    public void SendOverdueReservationReminders_ShouldSendEmailToMembersWithOverdueReservations()
    {
        var author = new Author() { Id = 1, LastName = "Test Author", FirstName = "Test Author" };
        var publisher = new Publisher() { Siret = "1234567890", Name = "Test Publisher" };
        var book = new Book() { Isbn = "2253009687", Title = "Test Book", Author = author, Publisher = publisher, Format = "Poche", IsAvailable = true };
        var member = new Member() { Code = 1, LastName = "Test Member", FirstName = "Test Member", BirthDate = new DateTime(2000, 1, 1), Civility = "M" };
        var overdueReservation1 = new Reservation() { Id = 1, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 2, 27) };
        var overdueReservation2 = new Reservation() { Id = 2, Book = book, Member = member, ReservationDate = new DateTime(2025, 2, 24), ReturnDate = new DateTime(2025, 2, 27) };

        _mockDatabaseService.Setup(service => service.GetOverdueReservations()).Returns(new List<Reservation> { overdueReservation1, overdueReservation2 });

        var reservationService = new ReservationService(_mockDatabaseService.Object, _mockEmailService.Object);
        reservationService.SendOverdueReservationReminders();

        _mockEmailService.Verify(service => service.SendReminderEmail(member, It.Is<List<Reservation>>(r => r.Count == 2)), Times.Once);
        Assert.That(overdueReservation1.ReturnDate, Is.LessThan(DateTime.Now));
        Assert.That(overdueReservation2.ReturnDate, Is.LessThan(DateTime.Now));
    }
    
    [Test]
    public void SendOverdueReservationReminders_ShouldNotSendEmailIfNoOverdueReservations()
    {
        _mockDatabaseService.Setup(service => service.GetOverdueReservations()).Returns(new List<Reservation>());
        var mockEmailService = new Mock<IEmailService>();

        var reservationService = new ReservationService(_mockDatabaseService.Object, mockEmailService.Object);
        reservationService.SendOverdueReservationReminders();

        mockEmailService.Verify(service => service.SendReminderEmail(It.IsAny<Member>(), It.IsAny<List<Reservation>>()), Times.Never);
        Assert.That(_mockDatabaseService.Object.GetOverdueReservations().Count, Is.EqualTo(0));
    }
    
    [Test]
    public void SendOverdueReservationReminders_ShouldSendSingleEmailPerMemberWithMultipleOverdueReservations()
    {
        var member = new Member() { Code = 1, LastName = "Test Member", FirstName = "Test Member" };
        var overdueReservation1 = new Reservation() { Id = 1, Member = member, ReservationDate = DateTime.Now.AddMonths(-2), ReturnDate = DateTime.Now.AddMonths(-1) };
        var overdueReservation2 = new Reservation() { Id = 2, Member = member, ReservationDate = DateTime.Now.AddMonths(-2), ReturnDate = DateTime.Now.AddMonths(-1) };

        _mockDatabaseService.Setup(service => service.GetOverdueReservations()).Returns(new List<Reservation> { overdueReservation1, overdueReservation2 });
        var mockEmailService = new Mock<IEmailService>();

        var reservationService = new ReservationService(_mockDatabaseService.Object, mockEmailService.Object);
        reservationService.SendOverdueReservationReminders();

        mockEmailService.Verify(service => service.SendReminderEmail(member, It.Is<List<Reservation>>(r => r.Count == 2)), Times.Once);
        Assert.That(overdueReservation1.ReturnDate, Is.LessThan(DateTime.Now));
        Assert.That(overdueReservation2.ReturnDate, Is.LessThan(DateTime.Now));
    }
}
