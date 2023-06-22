namespace ApiCartobani.FunctionalTests.FunctionalTests.DAs;

using ApiCartobani.SharedTestHelpers.Fakes.DA;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetDAListTests : TestBase
{
    [Test]
    public async Task get_da_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.DAs.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}