namespace ApiCartobani.FunctionalTests.FunctionalTests.Attributs;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateAttributTests : TestBase
{
    [Test]
    public async Task create_attribut_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeAttribut = new FakeAttributForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Attributs.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeAttribut);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}