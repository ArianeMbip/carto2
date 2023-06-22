namespace ApiCartobani.UnitTests.UnitTests.Domain.Contacts;

using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.Contacts.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateContactTests
{
    private readonly Faker _faker;

    public CreateContactTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_contact()
    {
        // Arrange
        var contactToCreate = new FakeContactForCreationDto().Generate();
        
        // Act
        var fakeContact = Contact.Create(contactToCreate);

        // Assert
        fakeContact.Nom.Should().Be(contactToCreate.Nom);
        fakeContact.Email.Should().Be(contactToCreate.Email);
        fakeContact.Entite.Should().Be(contactToCreate.Entite);
        fakeContact.Fonction.Should().Be(contactToCreate.Fonction);
        fakeContact.Telephone.Should().Be(contactToCreate.Telephone);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeContact = FakeContact.Generate();

        // Assert
        fakeContact.DomainEvents.Count.Should().Be(1);
        fakeContact.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ContactCreated));
    }
}