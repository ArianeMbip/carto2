namespace ApiCartobani.FunctionalTests.FunctionalTests.Environnements;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetEnvironnementTests : TestBase
{
    [Test]
    public async Task get_environnement_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeEnvironnement = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnement);

        // Act
        var route = ApiRoutes.Environnements.GetRecord.Replace(ApiRoutes.Environnements.Id, fakeEnvironnement.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}