namespace ApiCartobani.IntegrationTests.FeatureTests.Historiques;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.Historiques.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddHistoriqueCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_historique_to_db()
    {
        // Arrange
        var fakeHistoriqueOne = new FakeHistoriqueForCreationDto().Generate();

        // Act
        var command = new AddHistorique.Command(fakeHistoriqueOne);
        var historiqueReturned = await SendAsync(command);
        var historiqueCreated = await ExecuteDbContextAsync(db => db.Historiques
            .FirstOrDefaultAsync(h => h.Id == historiqueReturned.Id));

        // Assert
        historiqueReturned.DateModification.Should().BeCloseTo(fakeHistoriqueOne.DateModification, 1.Seconds());
        historiqueReturned.PartieModifiee.Should().Be(fakeHistoriqueOne.PartieModifiee);
        historiqueReturned.AncienneValeur.Should().Be(fakeHistoriqueOne.AncienneValeur);
        historiqueReturned.NouvelleValeur.Should().Be(fakeHistoriqueOne.NouvelleValeur);
        historiqueReturned.NomUtilisateur.Should().Be(fakeHistoriqueOne.NomUtilisateur);
        historiqueReturned.CUID.Should().Be(fakeHistoriqueOne.CUID);

        historiqueCreated.DateModification.Should().BeCloseTo(fakeHistoriqueOne.DateModification, 1.Seconds());
        historiqueCreated.PartieModifiee.Should().Be(fakeHistoriqueOne.PartieModifiee);
        historiqueCreated.AncienneValeur.Should().Be(fakeHistoriqueOne.AncienneValeur);
        historiqueCreated.NouvelleValeur.Should().Be(fakeHistoriqueOne.NouvelleValeur);
        historiqueCreated.NomUtilisateur.Should().Be(fakeHistoriqueOne.NomUtilisateur);
        historiqueCreated.CUID.Should().Be(fakeHistoriqueOne.CUID);
    }
}