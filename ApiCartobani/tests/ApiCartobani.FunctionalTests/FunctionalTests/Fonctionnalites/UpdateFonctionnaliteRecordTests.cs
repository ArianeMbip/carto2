namespace ApiCartobani.FunctionalTests.FunctionalTests.Fonctionnalites;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateFonctionnaliteRecordTests : TestBase
{
    [Test]
    public async Task put_fonctionnalite_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeFonctionnalite = FakeFonctionnalite.Generate(new FakeFonctionnaliteForCreationDto().Generate());
        var updatedFonctionnaliteDto = new FakeFonctionnaliteForUpdateDto().Generate();
        await InsertAsync(fakeFonctionnalite);

        // Act
        var route = ApiRoutes.Fonctionnalites.Put.Replace(ApiRoutes.Fonctionnalites.Id, fakeFonctionnalite.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedFonctionnaliteDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}