namespace ApiCartobani.UnitTests.UnitTests.Domain.GestionnaireActif;

using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateGestionnaireActifTests
{
    private readonly Faker _faker;

    public CreateGestionnaireActifTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_gestionnaireActif()
    {
        // Arrange
        var gestionnaireActifToCreate = new FakeGestionnaireActifForCreationDto().Generate();
        
        // Act
        var fakeGestionnaireActif = GestionnaireActif.Create(gestionnaireActifToCreate);

        // Assert
        fakeGestionnaireActif.Nom.Should().Be(gestionnaireActifToCreate.Nom);
        fakeGestionnaireActif.CUID.Should().Be(gestionnaireActifToCreate.CUID);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeGestionnaireActif = FakeGestionnaireActif.Generate();

        // Assert
        fakeGestionnaireActif.DomainEvents.Count.Should().Be(1);
        fakeGestionnaireActif.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(GestionnaireActifCreated));
    }
}