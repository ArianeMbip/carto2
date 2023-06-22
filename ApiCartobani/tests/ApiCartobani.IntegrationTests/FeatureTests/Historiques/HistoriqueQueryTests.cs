namespace ApiCartobani.IntegrationTests.FeatureTests.Historiques;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using ApiCartobani.Domain.Historiques.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class HistoriqueQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_historique_with_accurate_props()
    {
        // Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate(new FakeHistoriqueForCreationDto().Generate());
        await InsertAsync(fakeHistoriqueOne);

        // Act
        var query = new GetHistorique.Query(fakeHistoriqueOne.Id);
        var historique = await SendAsync(query);

        // Assert
        historique.DateModification.Should().BeCloseTo(fakeHistoriqueOne.DateModification, 1.Seconds());
        historique.PartieModifiee.Should().Be(fakeHistoriqueOne.PartieModifiee);
        historique.AncienneValeur.Should().Be(fakeHistoriqueOne.AncienneValeur);
        historique.NouvelleValeur.Should().Be(fakeHistoriqueOne.NouvelleValeur);
        historique.NomUtilisateur.Should().Be(fakeHistoriqueOne.NomUtilisateur);
        historique.CUID.Should().Be(fakeHistoriqueOne.CUID);
    }

    [Test]
    public async Task get_historique_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetHistorique.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}