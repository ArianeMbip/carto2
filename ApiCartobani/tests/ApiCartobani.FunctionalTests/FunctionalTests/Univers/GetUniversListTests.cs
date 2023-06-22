namespace ApiCartobani.FunctionalTests.FunctionalTests.Univers;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetUniversListTests : TestBase
{
    [Test]
    public async Task get_univers_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Univers.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}