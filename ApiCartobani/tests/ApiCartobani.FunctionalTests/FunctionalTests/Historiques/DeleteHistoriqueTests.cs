namespace ApiCartobani.FunctionalTests.FunctionalTests.Historiques;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteHistoriqueTests : TestBase
{
    [Test]
    public async Task delete_historique_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeHistorique = FakeHistorique.Generate(new FakeHistoriqueForCreationDto().Generate());
        await InsertAsync(fakeHistorique);

        // Act
        var route = ApiRoutes.Historiques.Delete.Replace(ApiRoutes.Historiques.Id, fakeHistorique.Id.ToString());
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}