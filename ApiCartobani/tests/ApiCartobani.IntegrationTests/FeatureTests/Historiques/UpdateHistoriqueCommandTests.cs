namespace ApiCartobani.IntegrationTests.FeatureTests.Historiques;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using ApiCartobani.Domain.Historiques.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Historiques.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateHistoriqueCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_historique_in_db()
    {
        // Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate(new FakeHistoriqueForCreationDto().Generate());
        var updatedHistoriqueDto = new FakeHistoriqueForUpdateDto().Generate();
        await InsertAsync(fakeHistoriqueOne);

        var historique = await ExecuteDbContextAsync(db => db.Historiques
            .FirstOrDefaultAsync(h => h.Id == fakeHistoriqueOne.Id));
        var id = historique.Id;

        // Act
        var command = new UpdateHistorique.Command(id, updatedHistoriqueDto);
        await SendAsync(command);
        var updatedHistorique = await ExecuteDbContextAsync(db => db.Historiques.FirstOrDefaultAsync(h => h.Id == id));

        // Assert
        updatedHistorique.DateModification.Should().BeCloseTo(updatedHistoriqueDto.DateModification, 1.Seconds());
        updatedHistorique.PartieModifiee.Should().Be(updatedHistoriqueDto.PartieModifiee);
        updatedHistorique.AncienneValeur.Should().Be(updatedHistoriqueDto.AncienneValeur);
        updatedHistorique.NouvelleValeur.Should().Be(updatedHistoriqueDto.NouvelleValeur);
        updatedHistorique.NomUtilisateur.Should().Be(updatedHistoriqueDto.NomUtilisateur);
        updatedHistorique.CUID.Should().Be(updatedHistoriqueDto.CUID);
    }
}