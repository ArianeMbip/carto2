namespace ApiCartobani.FunctionalTests.FunctionalTests.Historiques;

using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetHistoriqueListTests : TestBase
{
    [Test]
    public async Task get_historique_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Historiques.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}