namespace ApiCartobani.FunctionalTests.FunctionalTests.Environnements;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteEnvironnementTests : TestBase
{
    [Test]
    public async Task delete_environnement_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeEnvironnement = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnement);

        // Act
        var route = ApiRoutes.Environnements.Delete.Replace(ApiRoutes.Environnements.Id, fakeEnvironnement.Id.ToString());
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}