namespace ApiCartobani.FunctionalTests.FunctionalTests.Historiques;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetHistoriqueTests : TestBase
{
    [Test]
    public async Task get_historique_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeHistorique = FakeHistorique.Generate(new FakeHistoriqueForCreationDto().Generate());
        await InsertAsync(fakeHistorique);

        // Act
        var route = ApiRoutes.Historiques.GetRecord.Replace(ApiRoutes.Historiques.Id, fakeHistorique.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}