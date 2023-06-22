namespace ApiCartobani.FunctionalTests.FunctionalTests.Icones;

using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateIconeTests : TestBase
{
    [Test]
    public async Task create_icone_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeIcone = new FakeIconeForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Icones.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeIcone);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}