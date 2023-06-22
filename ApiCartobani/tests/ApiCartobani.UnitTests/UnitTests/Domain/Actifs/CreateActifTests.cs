namespace ApiCartobani.UnitTests.UnitTests.Domain.Actifs;

using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.Actifs.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateActifTests
{
    private readonly Faker _faker;

    public CreateActifTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_actif()
    {
        // Arrange
        var actifToCreate = new FakeActifForCreationDto().Generate();
        
        // Act
        var fakeActif = Actif.Create(actifToCreate);

        // Assert
        fakeActif.Nom.Should().Be(actifToCreate.Nom);
        fakeActif.Criticite.Should().Be(actifToCreate.Criticite);
        fakeActif.Description.Should().Be(actifToCreate.Description);
        fakeActif.Version.Should().Be(actifToCreate.Version);
        fakeActif.Icone.Should().Be(actifToCreate.Icone);
        fakeActif.Statut.Should().Be(actifToCreate.Statut);
        fakeActif.TypeActif.Should().Be(actifToCreate.TypeActif);
        fakeActif.PreVersion.Should().Be(actifToCreate.PreVersion);
        fakeActif.Hierarchie.Should().Be(actifToCreate.Hierarchie);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeActif = FakeActif.Generate();

        // Assert
        fakeActif.DomainEvents.Count.Should().Be(1);
        fakeActif.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ActifCreated));
    }
}