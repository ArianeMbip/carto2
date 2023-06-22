namespace ApiCartobani.UnitTests.UnitTests.Domain.Historiques;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.Historiques.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateHistoriqueTests
{
    private readonly Faker _faker;

    public CreateHistoriqueTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_historique()
    {
        // Arrange
        var historiqueToCreate = new FakeHistoriqueForCreationDto().Generate();
        
        // Act
        var fakeHistorique = Historique.Create(historiqueToCreate);

        // Assert
        fakeHistorique.DateModification.Should().BeCloseTo(historiqueToCreate.DateModification, 1.Seconds());
        fakeHistorique.PartieModifiee.Should().Be(historiqueToCreate.PartieModifiee);
        fakeHistorique.AncienneValeur.Should().Be(historiqueToCreate.AncienneValeur);
        fakeHistorique.NouvelleValeur.Should().Be(historiqueToCreate.NouvelleValeur);
        fakeHistorique.NomUtilisateur.Should().Be(historiqueToCreate.NomUtilisateur);
        fakeHistorique.CUID.Should().Be(historiqueToCreate.CUID);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeHistorique = FakeHistorique.Generate();

        // Assert
        fakeHistorique.DomainEvents.Count.Should().Be(1);
        fakeHistorique.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(HistoriqueCreated));
    }
}