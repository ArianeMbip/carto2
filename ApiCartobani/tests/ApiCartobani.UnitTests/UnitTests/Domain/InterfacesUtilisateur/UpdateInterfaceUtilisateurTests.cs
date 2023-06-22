namespace ApiCartobani.UnitTests.UnitTests.Domain.InterfacesUtilisateur;

using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateInterfaceUtilisateurTests
{
    private readonly Faker _faker;

    public UpdateInterfaceUtilisateurTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_interfaceUtilisateur()
    {
        // Arrange
        var fakeInterfaceUtilisateur = FakeInterfaceUtilisateur.Generate();
        var updatedInterfaceUtilisateur = new FakeInterfaceUtilisateurForUpdateDto().Generate();
        
        // Act
        fakeInterfaceUtilisateur.Update(updatedInterfaceUtilisateur);

        // Assert
        fakeInterfaceUtilisateur.Nom.Should().Be(updatedInterfaceUtilisateur.Nom);
        fakeInterfaceUtilisateur.Image.Should().Be(updatedInterfaceUtilisateur.Image);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeInterfaceUtilisateur = FakeInterfaceUtilisateur.Generate();
        var updatedInterfaceUtilisateur = new FakeInterfaceUtilisateurForUpdateDto().Generate();
        fakeInterfaceUtilisateur.DomainEvents.Clear();
        
        // Act
        fakeInterfaceUtilisateur.Update(updatedInterfaceUtilisateur);

        // Assert
        fakeInterfaceUtilisateur.DomainEvents.Count.Should().Be(1);
        fakeInterfaceUtilisateur.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(InterfaceUtilisateurUpdated));
    }
}