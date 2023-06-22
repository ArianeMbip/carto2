namespace ApiCartobani.FunctionalTests.FunctionalTests.TypeElements;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetTypeElementListTests : TestBase
{
    [Test]
    public async Task get_typeelement_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.TypeElements.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}