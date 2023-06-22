namespace ApiCartobani.UnitTests.UnitTests.Domain.Historiques;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.Historiques.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateHistoriqueTests
{
    private readonly Faker _faker;

    public UpdateHistoriqueTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_historique()
    {
        // Arrange
        var fakeHistorique = FakeHistorique.Generate();
        var updatedHistorique = new FakeHistoriqueForUpdateDto().Generate();
        
        // Act
        fakeHistorique.Update(updatedHistorique);

        // Assert
        fakeHistorique.DateModification.Should().BeCloseTo(updatedHistorique.DateModification, 1.Seconds());
        fakeHistorique.PartieModifiee.Should().Be(updatedHistorique.PartieModifiee);
        fakeHistorique.AncienneValeur.Should().Be(updatedHistorique.AncienneValeur);
        fakeHistorique.NouvelleValeur.Should().Be(updatedHistorique.NouvelleValeur);
        fakeHistorique.NomUtilisateur.Should().Be(updatedHistorique.NomUtilisateur);
        fakeHistorique.CUID.Should().Be(updatedHistorique.CUID);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeHistorique = FakeHistorique.Generate();
        var updatedHistorique = new FakeHistoriqueForUpdateDto().Generate();
        fakeHistorique.DomainEvents.Clear();
        
        // Act
        fakeHistorique.Update(updatedHistorique);

        // Assert
        fakeHistorique.DomainEvents.Count.Should().Be(1);
        fakeHistorique.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(HistoriqueUpdated));
    }
}