namespace ApiCartobani.FunctionalTests.FunctionalTests.Attributs;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteAttributTests : TestBase
{
    [Test]
    public async Task delete_attribut_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeAttribut = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttribut);

        // Act
        var route = ApiRoutes.Attributs.Delete.Replace(ApiRoutes.Attributs.Id, fakeAttribut.Id.ToString());
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}