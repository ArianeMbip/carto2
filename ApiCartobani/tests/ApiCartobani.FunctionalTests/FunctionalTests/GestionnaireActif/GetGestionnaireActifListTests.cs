namespace ApiCartobani.FunctionalTests.FunctionalTests.GestionnaireActif;

using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetGestionnaireActifListTests : TestBase
{
    [Test]
    public async Task get_gestionnaireactif_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.GestionnaireActif.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}