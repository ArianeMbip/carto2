namespace ApiCartobani.FunctionalTests.FunctionalTests.GestionnaireActif;

using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateGestionnaireActifRecordTests : TestBase
{
    [Test]
    public async Task put_gestionnaireactif_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeGestionnaireActif = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto().Generate());
        var updatedGestionnaireActifDto = new FakeGestionnaireActifForUpdateDto().Generate();
        await InsertAsync(fakeGestionnaireActif);

        // Act
        var route = ApiRoutes.GestionnaireActif.Put.Replace(ApiRoutes.GestionnaireActif.Id, fakeGestionnaireActif.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedGestionnaireActifDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}