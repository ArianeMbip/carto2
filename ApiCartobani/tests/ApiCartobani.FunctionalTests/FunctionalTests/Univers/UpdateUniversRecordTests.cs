namespace ApiCartobani.FunctionalTests.FunctionalTests.Univers;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateUniversRecordTests : TestBase
{
    [Test]
    public async Task put_univers_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUnivers = FakeUnivers.Generate(new FakeUniversForCreationDto().Generate());
        var updatedUniversDto = new FakeUniversForUpdateDto().Generate();
        await InsertAsync(fakeUnivers);

        // Act
        var route = ApiRoutes.Univers.Put.Replace(ApiRoutes.Univers.Id, fakeUnivers.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedUniversDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}