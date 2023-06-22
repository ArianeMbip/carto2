namespace ApiCartobani.UnitTests.UnitTests.Domain.Contacts.Features;

using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.Contacts.Dtos;
using ApiCartobani.Domain.Contacts.Mappings;
using ApiCartobani.Domain.Contacts.Features;
using ApiCartobani.Domain.Contacts.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetContactListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IContactRepository> _contactRepository;

    public GetContactListTests()
    {
        _contactRepository = new Mock<IContactRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_contact()
    {
        //Arrange
        var fakeContactOne = FakeContact.Generate();
        var fakeContactTwo = FakeContact.Generate();
        var fakeContactThree = FakeContact.Generate();
        var contact = new List<Contact>();
        contact.Add(fakeContactOne);
        contact.Add(fakeContactTwo);
        contact.Add(fakeContactThree);
        var mockDbData = contact.AsQueryable().BuildMock();
        
        var queryParameters = new ContactParametersDto() { PageSize = 1, PageNumber = 2 };

        _contactRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetContactList.Query(queryParameters);
        var handler = new GetContactList.Handler(_contactRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }

    [Test]
    public async Task can_filter_contact_list_using_Nom()
    {
        //Arrange
        var fakeContactOne = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Nom, _ => "alpha")
            .Generate());
        var fakeContactTwo = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new ContactParametersDto() { Filters = $"Nom == {fakeContactTwo.Nom}" };

        var contactList = new List<Contact>() { fakeContactOne, fakeContactTwo };
        var mockDbData = contactList.AsQueryable().BuildMock();

        _contactRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetContactList.Query(queryParameters);
        var handler = new GetContactList.Handler(_contactRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeContactTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_contact_list_using_Email()
    {
        //Arrange
        var fakeContactOne = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Email, _ => "alpha")
            .Generate());
        var fakeContactTwo = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Email, _ => "bravo")
            .Generate());
        var queryParameters = new ContactParametersDto() { Filters = $"Email == {fakeContactTwo.Email}" };

        var contactList = new List<Contact>() { fakeContactOne, fakeContactTwo };
        var mockDbData = contactList.AsQueryable().BuildMock();

        _contactRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetContactList.Query(queryParameters);
        var handler = new GetContactList.Handler(_contactRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeContactTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_contact_list_using_Telephone()
    {
        //Arrange
        var fakeContactOne = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Telephone, _ => "alpha")
            .Generate());
        var fakeContactTwo = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Telephone, _ => "bravo")
            .Generate());
        var queryParameters = new ContactParametersDto() { Filters = $"Telephone == {fakeContactTwo.Telephone}" };

        var contactList = new List<Contact>() { fakeContactOne, fakeContactTwo };
        var mockDbData = contactList.AsQueryable().BuildMock();

        _contactRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetContactList.Query(queryParameters);
        var handler = new GetContactList.Handler(_contactRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
        response
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeContactTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_contact_by_Nom()
    {
        //Arrange
        var fakeContactOne = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Nom, _ => "alpha")
            .Generate());
        var fakeContactTwo = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Nom, _ => "bravo")
            .Generate());
        var queryParameters = new ContactParametersDto() { SortOrder = "-Nom" };

        var ContactList = new List<Contact>() { fakeContactOne, fakeContactTwo };
        var mockDbData = ContactList.AsQueryable().BuildMock();

        _contactRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetContactList.Query(queryParameters);
        var handler = new GetContactList.Handler(_contactRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeContactTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeContactOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_contact_by_Email()
    {
        //Arrange
        var fakeContactOne = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Email, _ => "alpha")
            .Generate());
        var fakeContactTwo = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Email, _ => "bravo")
            .Generate());
        var queryParameters = new ContactParametersDto() { SortOrder = "-Email" };

        var ContactList = new List<Contact>() { fakeContactOne, fakeContactTwo };
        var mockDbData = ContactList.AsQueryable().BuildMock();

        _contactRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetContactList.Query(queryParameters);
        var handler = new GetContactList.Handler(_contactRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeContactTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeContactOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_contact_by_Telephone()
    {
        //Arrange
        var fakeContactOne = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Telephone, _ => "alpha")
            .Generate());
        var fakeContactTwo = FakeContact.Generate(new FakeContactForCreationDto()
            .RuleFor(c => c.Telephone, _ => "bravo")
            .Generate());
        var queryParameters = new ContactParametersDto() { SortOrder = "-Telephone" };

        var ContactList = new List<Contact>() { fakeContactOne, fakeContactTwo };
        var mockDbData = ContactList.AsQueryable().BuildMock();

        _contactRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);

        //Act
        var query = new GetContactList.Query(queryParameters);
        var handler = new GetContactList.Handler(_contactRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.FirstOrDefault()
            .Should().BeEquivalentTo(fakeContactTwo, options =>
                options.ExcludingMissingMembers());
        response.Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeContactOne, options =>
                options.ExcludingMissingMembers());
    }
}