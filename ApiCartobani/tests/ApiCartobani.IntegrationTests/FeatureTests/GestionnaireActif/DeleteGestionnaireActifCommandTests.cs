namespace ApiCartobani.IntegrationTests.FeatureTests.GestionnaireActif;

using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteGestionnaireActifCommandTests : TestBase
{
    [Test]
    public async Task can_delete_gestionnaireactif_from_db()
    {
        // Arrange
        var fakeGestionnaireActifOne = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto().Generate());
        await InsertAsync(fakeGestionnaireActifOne);
        var gestionnaireActif = await ExecuteDbContextAsync(db => db.GestionnaireActif
            .FirstOrDefaultAsync(G => G.Id == fakeGestionnaireActifOne.Id));

        // Act
        var command = new DeleteGestionnaireActif.Command(gestionnaireActif.Id);
        await SendAsync(command);
        var gestionnaireActifResponse = await ExecuteDbContextAsync(db => db.GestionnaireActif.CountAsync(G => G.Id == gestionnaireActif.Id));

        // Assert
        gestionnaireActifResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_gestionnaireactif_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteGestionnaireActif.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_gestionnaireactif_from_db()
    {
        // Arrange
        var fakeGestionnaireActifOne = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto().Generate());
        await InsertAsync(fakeGestionnaireActifOne);
        var gestionnaireActif = await ExecuteDbContextAsync(db => db.GestionnaireActif
            .FirstOrDefaultAsync(G => G.Id == fakeGestionnaireActifOne.Id));

        // Act
        var command = new DeleteGestionnaireActif.Command(gestionnaireActif.Id);
        await SendAsync(command);
        var deletedGestionnaireActif = await ExecuteDbContextAsync(db => db.GestionnaireActif
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == gestionnaireActif.Id));

        // Assert
        deletedGestionnaireActif?.IsDeleted.Should().BeTrue();
    }
}