namespace ApiCartobani.FunctionalTests.FunctionalTests.Historiques;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateHistoriqueRecordTests : TestBase
{
    [Test]
    public async Task put_historique_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeHistorique = FakeHistorique.Generate(new FakeHistoriqueForCreationDto().Generate());
        var updatedHistoriqueDto = new FakeHistoriqueForUpdateDto().Generate();
        await InsertAsync(fakeHistorique);

        // Act
        var route = ApiRoutes.Historiques.Put.Replace(ApiRoutes.Historiques.Id, fakeHistorique.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedHistoriqueDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}