namespace ApiCartobani.FunctionalTests.FunctionalTests.Attributs;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateAttributRecordTests : TestBase
{
    [Test]
    public async Task put_attribut_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeAttribut = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        var updatedAttributDto = new FakeAttributForUpdateDto().Generate();
        await InsertAsync(fakeAttribut);

        // Act
        var route = ApiRoutes.Attributs.Put.Replace(ApiRoutes.Attributs.Id, fakeAttribut.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedAttributDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}