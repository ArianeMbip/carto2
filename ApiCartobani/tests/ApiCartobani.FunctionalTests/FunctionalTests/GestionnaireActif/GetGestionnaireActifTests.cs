namespace ApiCartobani.FunctionalTests.FunctionalTests.GestionnaireActif;

using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetGestionnaireActifTests : TestBase
{
    [Test]
    public async Task get_gestionnaireactif_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeGestionnaireActif = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto().Generate());
        await InsertAsync(fakeGestionnaireActif);

        // Act
        var route = ApiRoutes.GestionnaireActif.GetRecord.Replace(ApiRoutes.GestionnaireActif.Id, fakeGestionnaireActif.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}