namespace ApiCartobani.FunctionalTests.FunctionalTests.Fonctionnalites;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteFonctionnaliteTests : TestBase
{
    [Test]
    public async Task delete_fonctionnalite_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeFonctionnalite = FakeFonctionnalite.Generate(new FakeFonctionnaliteForCreationDto().Generate());
        await InsertAsync(fakeFonctionnalite);

        // Act
        var route = ApiRoutes.Fonctionnalites.Delete.Replace(ApiRoutes.Fonctionnalites.Id, fakeFonctionnalite.Id.ToString());
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}