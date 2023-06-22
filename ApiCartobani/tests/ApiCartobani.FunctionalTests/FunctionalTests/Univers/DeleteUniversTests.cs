namespace ApiCartobani.FunctionalTests.FunctionalTests.Univers;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteUniversTests : TestBase
{
    [Test]
    public async Task delete_univers_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUnivers = FakeUnivers.Generate(new FakeUniversForCreationDto().Generate());
        await InsertAsync(fakeUnivers);

        // Act
        var route = ApiRoutes.Univers.Delete.Replace(ApiRoutes.Univers.Id, fakeUnivers.Id.ToString());
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}