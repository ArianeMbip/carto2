namespace ApiCartobani.UnitTests.UnitTests.Domain.Actifs;

using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.Actifs.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateActifTests
{
    private readonly Faker _faker;

    public UpdateActifTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_actif()
    {
        // Arrange
        var fakeActif = FakeActif.Generate();
        var updatedActif = new FakeActifForUpdateDto().Generate();
        
        // Act
        fakeActif.Update(updatedActif);

        // Assert
        fakeActif.Nom.Should().Be(updatedActif.Nom);
        fakeActif.Criticite.Should().Be(updatedActif.Criticite);
        fakeActif.Description.Should().Be(updatedActif.Description);
        fakeActif.Version.Should().Be(updatedActif.Version);
        fakeActif.Icone.Should().Be(updatedActif.Icone);
        fakeActif.Statut.Should().Be(updatedActif.Statut);
        fakeActif.TypeActif.Should().Be(updatedActif.TypeActif);
        fakeActif.PreVersion.Should().Be(updatedActif.PreVersion);
        fakeActif.Hierarchie.Should().Be(updatedActif.Hierarchie);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeActif = FakeActif.Generate();
        var updatedActif = new FakeActifForUpdateDto().Generate();
        fakeActif.DomainEvents.Clear();
        
        // Act
        fakeActif.Update(updatedActif);

        // Assert
        fakeActif.DomainEvents.Count.Should().Be(1);
        fakeActif.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ActifUpdated));
    }
}