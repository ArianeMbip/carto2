namespace ApiCartobani.FunctionalTests.FunctionalTests.TypeElements;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateTypeElementTests : TestBase
{
    [Test]
    public async Task create_typeelement_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeTypeElement = new FakeTypeElementForCreationDto().Generate();

        // Act
        var route = ApiRoutes.TypeElements.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeTypeElement);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}