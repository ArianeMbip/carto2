namespace ApiCartobani.FunctionalTests.FunctionalTests.Fonctionnalites;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetFonctionnaliteTests : TestBase
{
    [Test]
    public async Task get_fonctionnalite_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeFonctionnalite = FakeFonctionnalite.Generate(new FakeFonctionnaliteForCreationDto().Generate());
        await InsertAsync(fakeFonctionnalite);

        // Act
        var route = ApiRoutes.Fonctionnalites.GetRecord.Replace(ApiRoutes.Fonctionnalites.Id, fakeFonctionnalite.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}