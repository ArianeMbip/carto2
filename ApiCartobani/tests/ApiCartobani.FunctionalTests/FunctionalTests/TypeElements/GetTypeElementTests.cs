namespace ApiCartobani.FunctionalTests.FunctionalTests.TypeElements;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetTypeElementTests : TestBase
{
    [Test]
    public async Task get_typeelement_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeTypeElement = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElement);

        // Act
        var route = ApiRoutes.TypeElements.GetRecord.Replace(ApiRoutes.TypeElements.Id, fakeTypeElement.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}