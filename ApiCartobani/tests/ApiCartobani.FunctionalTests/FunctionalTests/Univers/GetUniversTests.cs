namespace ApiCartobani.FunctionalTests.FunctionalTests.Univers;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetUniversTests : TestBase
{
    [Test]
    public async Task get_univers_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeUnivers = FakeUnivers.Generate(new FakeUniversForCreationDto().Generate());
        await InsertAsync(fakeUnivers);

        // Act
        var route = ApiRoutes.Univers.GetRecord.Replace(ApiRoutes.Univers.Id, fakeUnivers.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}