namespace ApiCartobani.IntegrationTests.FeatureTests.GestionnaireActif;

using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.GestionnaireActif.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddGestionnaireActifCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_gestionnaireactif_to_db()
    {
        // Arrange
        var fakeGestionnaireActifOne = new FakeGestionnaireActifForCreationDto().Generate();

        // Act
        var command = new AddGestionnaireActif.Command(fakeGestionnaireActifOne);
        var gestionnaireActifReturned = await SendAsync(command);
        var gestionnaireActifCreated = await ExecuteDbContextAsync(db => db.GestionnaireActif
            .FirstOrDefaultAsync(G => G.Id == gestionnaireActifReturned.Id));

        // Assert
        gestionnaireActifReturned.Nom.Should().Be(fakeGestionnaireActifOne.Nom);
        gestionnaireActifReturned.CUID.Should().Be(fakeGestionnaireActifOne.CUID);

        gestionnaireActifCreated.Nom.Should().Be(fakeGestionnaireActifOne.Nom);
        gestionnaireActifCreated.CUID.Should().Be(fakeGestionnaireActifOne.CUID);
    }
}