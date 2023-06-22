namespace ApiCartobani.FunctionalTests.FunctionalTests.Environnements;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateEnvironnementTests : TestBase
{
    [Test]
    public async Task create_environnement_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeEnvironnement = new FakeEnvironnementForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Environnements.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeEnvironnement);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}