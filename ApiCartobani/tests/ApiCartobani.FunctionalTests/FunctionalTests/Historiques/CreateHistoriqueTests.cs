namespace ApiCartobani.FunctionalTests.FunctionalTests.Historiques;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateHistoriqueTests : TestBase
{
    [Test]
    public async Task create_historique_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeHistorique = new FakeHistoriqueForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Historiques.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeHistorique);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}