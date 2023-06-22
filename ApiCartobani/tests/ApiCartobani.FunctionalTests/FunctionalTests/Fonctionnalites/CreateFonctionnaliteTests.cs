namespace ApiCartobani.FunctionalTests.FunctionalTests.Fonctionnalites;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateFonctionnaliteTests : TestBase
{
    [Test]
    public async Task create_fonctionnalite_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeFonctionnalite = new FakeFonctionnaliteForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Fonctionnalites.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeFonctionnalite);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}