namespace ApiCartobani.UnitTests.UnitTests.Domain.Contacts;

using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.Contacts.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateContactTests
{
    private readonly Faker _faker;

    public UpdateContactTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_contact()
    {
        // Arrange
        var fakeContact = FakeContact.Generate();
        var updatedContact = new FakeContactForUpdateDto().Generate();
        
        // Act
        fakeContact.Update(updatedContact);

        // Assert
        fakeContact.Nom.Should().Be(updatedContact.Nom);
        fakeContact.Email.Should().Be(updatedContact.Email);
        fakeContact.Entite.Should().Be(updatedContact.Entite);
        fakeContact.Fonction.Should().Be(updatedContact.Fonction);
        fakeContact.Telephone.Should().Be(updatedContact.Telephone);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeContact = FakeContact.Generate();
        var updatedContact = new FakeContactForUpdateDto().Generate();
        fakeContact.DomainEvents.Clear();
        
        // Act
        fakeContact.Update(updatedContact);

        // Assert
        fakeContact.DomainEvents.Count.Should().Be(1);
        fakeContact.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ContactUpdated));
    }
}