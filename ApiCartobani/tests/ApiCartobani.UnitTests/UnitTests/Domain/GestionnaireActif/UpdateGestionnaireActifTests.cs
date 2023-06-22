namespace ApiCartobani.UnitTests.UnitTests.Domain.GestionnaireActif;

using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateGestionnaireActifTests
{
    private readonly Faker _faker;

    public UpdateGestionnaireActifTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_gestionnaireActif()
    {
        // Arrange
        var fakeGestionnaireActif = FakeGestionnaireActif.Generate();
        var updatedGestionnaireActif = new FakeGestionnaireActifForUpdateDto().Generate();
        
        // Act
        fakeGestionnaireActif.Update(updatedGestionnaireActif);

        // Assert
        fakeGestionnaireActif.Nom.Should().Be(updatedGestionnaireActif.Nom);
        fakeGestionnaireActif.CUID.Should().Be(updatedGestionnaireActif.CUID);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeGestionnaireActif = FakeGestionnaireActif.Generate();
        var updatedGestionnaireActif = new FakeGestionnaireActifForUpdateDto().Generate();
        fakeGestionnaireActif.DomainEvents.Clear();
        
        // Act
        fakeGestionnaireActif.Update(updatedGestionnaireActif);

        // Assert
        fakeGestionnaireActif.DomainEvents.Count.Should().Be(1);
        fakeGestionnaireActif.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(GestionnaireActifUpdated));
    }
}