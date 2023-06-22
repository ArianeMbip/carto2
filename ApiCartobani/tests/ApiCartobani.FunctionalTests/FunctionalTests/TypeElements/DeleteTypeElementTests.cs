namespace ApiCartobani.FunctionalTests.FunctionalTests.TypeElements;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteTypeElementTests : TestBase
{
    [Test]
    public async Task delete_typeelement_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeTypeElement = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElement);

        // Act
        var route = ApiRoutes.TypeElements.Delete.Replace(ApiRoutes.TypeElements.Id, fakeTypeElement.Id.ToString());
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}