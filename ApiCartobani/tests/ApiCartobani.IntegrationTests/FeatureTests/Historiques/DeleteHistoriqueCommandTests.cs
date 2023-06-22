namespace ApiCartobani.IntegrationTests.FeatureTests.Historiques;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using ApiCartobani.Domain.Historiques.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteHistoriqueCommandTests : TestBase
{
    [Test]
    public async Task can_delete_historique_from_db()
    {
        // Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate(new FakeHistoriqueForCreationDto().Generate());
        await InsertAsync(fakeHistoriqueOne);
        var historique = await ExecuteDbContextAsync(db => db.Historiques
            .FirstOrDefaultAsync(h => h.Id == fakeHistoriqueOne.Id));

        // Act
        var command = new DeleteHistorique.Command(historique.Id);
        await SendAsync(command);
        var historiqueResponse = await ExecuteDbContextAsync(db => db.Historiques.CountAsync(h => h.Id == historique.Id));

        // Assert
        historiqueResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_historique_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteHistorique.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_historique_from_db()
    {
        // Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate(new FakeHistoriqueForCreationDto().Generate());
        await InsertAsync(fakeHistoriqueOne);
        var historique = await ExecuteDbContextAsync(db => db.Historiques
            .FirstOrDefaultAsync(h => h.Id == fakeHistoriqueOne.Id));

        // Act
        var command = new DeleteHistorique.Command(historique.Id);
        await SendAsync(command);
        var deletedHistorique = await ExecuteDbContextAsync(db => db.Historiques
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == historique.Id));

        // Assert
        deletedHistorique?.IsDeleted.Should().BeTrue();
    }
}