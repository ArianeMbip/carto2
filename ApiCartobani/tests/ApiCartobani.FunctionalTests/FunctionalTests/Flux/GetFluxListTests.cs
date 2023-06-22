namespace ApiCartobani.FunctionalTests.FunctionalTests.Flux;

using ApiCartobani.SharedTestHelpers.Fakes.Flux;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetFluxListTests : TestBase
{
    [Test]
    public async Task get_flux_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Flux.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}