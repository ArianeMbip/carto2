namespace ApiCartobani.FunctionalTests.FunctionalTests.TypeElements;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateTypeElementRecordTests : TestBase
{
    [Test]
    public async Task put_typeelement_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeTypeElement = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        var updatedTypeElementDto = new FakeTypeElementForUpdateDto().Generate();
        await InsertAsync(fakeTypeElement);

        // Act
        var route = ApiRoutes.TypeElements.Put.Replace(ApiRoutes.TypeElements.Id, fakeTypeElement.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedTypeElementDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}