namespace ApiCartobani.FunctionalTests.FunctionalTests.GestionnaireActif;

using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateGestionnaireActifTests : TestBase
{
    [Test]
    public async Task create_gestionnaireactif_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeGestionnaireActif = new FakeGestionnaireActifForCreationDto().Generate();

        // Act
        var route = ApiRoutes.GestionnaireActif.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeGestionnaireActif);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}