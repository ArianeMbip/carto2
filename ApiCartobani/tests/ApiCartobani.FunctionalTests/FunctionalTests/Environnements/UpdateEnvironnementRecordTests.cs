namespace ApiCartobani.FunctionalTests.FunctionalTests.Environnements;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateEnvironnementRecordTests : TestBase
{
    [Test]
    public async Task put_environnement_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeEnvironnement = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        var updatedEnvironnementDto = new FakeEnvironnementForUpdateDto().Generate();
        await InsertAsync(fakeEnvironnement);

        // Act
        var route = ApiRoutes.Environnements.Put.Replace(ApiRoutes.Environnements.Id, fakeEnvironnement.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedEnvironnementDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}