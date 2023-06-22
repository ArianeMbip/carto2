namespace ApiCartobani.UnitTests.UnitTests.Domain.InterfacesUtilisateur;

using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateInterfaceUtilisateurTests
{
    private readonly Faker _faker;

    public CreateInterfaceUtilisateurTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_interfaceUtilisateur()
    {
        // Arrange
        var interfaceUtilisateurToCreate = new FakeInterfaceUtilisateurForCreationDto().Generate();
        
        // Act
        var fakeInterfaceUtilisateur = InterfaceUtilisateur.Create(interfaceUtilisateurToCreate);

        // Assert
        fakeInterfaceUtilisateur.Nom.Should().Be(interfaceUtilisateurToCreate.Nom);
        fakeInterfaceUtilisateur.Image.Should().Be(interfaceUtilisateurToCreate.Image);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeInterfaceUtilisateur = FakeInterfaceUtilisateur.Generate();

        // Assert
        fakeInterfaceUtilisateur.DomainEvents.Count.Should().Be(1);
        fakeInterfaceUtilisateur.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(InterfaceUtilisateurCreated));
    }
}