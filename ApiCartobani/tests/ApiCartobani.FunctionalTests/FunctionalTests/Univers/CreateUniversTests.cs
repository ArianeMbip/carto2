namespace ApiCartobani.FunctionalTests.FunctionalTests.Univers;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateUniversTests : TestBase
{
    [Test]
    public async Task create_univers_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeUnivers = new FakeUniversForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Univers.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeUnivers);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}