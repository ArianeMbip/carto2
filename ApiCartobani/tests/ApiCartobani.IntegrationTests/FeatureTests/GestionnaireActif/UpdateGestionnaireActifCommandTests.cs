namespace ApiCartobani.IntegrationTests.FeatureTests.GestionnaireActif;

using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.GestionnaireActif.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateGestionnaireActifCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_gestionnaireactif_in_db()
    {
        // Arrange
        var fakeGestionnaireActifOne = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto().Generate());
        var updatedGestionnaireActifDto = new FakeGestionnaireActifForUpdateDto().Generate();
        await InsertAsync(fakeGestionnaireActifOne);

        var gestionnaireActif = await ExecuteDbContextAsync(db => db.GestionnaireActif
            .FirstOrDefaultAsync(G => G.Id == fakeGestionnaireActifOne.Id));
        var id = gestionnaireActif.Id;

        // Act
        var command = new UpdateGestionnaireActif.Command(id, updatedGestionnaireActifDto);
        await SendAsync(command);
        var updatedGestionnaireActif = await ExecuteDbContextAsync(db => db.GestionnaireActif.FirstOrDefaultAsync(G => G.Id == id));

        // Assert
        updatedGestionnaireActif.Nom.Should().Be(updatedGestionnaireActifDto.Nom);
        updatedGestionnaireActif.CUID.Should().Be(updatedGestionnaireActifDto.CUID);
    }
}