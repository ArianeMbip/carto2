namespace ApiCartobani.FunctionalTests.FunctionalTests.Actifs;

using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetActifListTests : TestBase
{
    [Test]
    public async Task get_actif_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Actifs.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}