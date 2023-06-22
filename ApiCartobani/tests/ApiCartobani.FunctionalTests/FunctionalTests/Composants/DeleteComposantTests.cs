namespace ApiCartobani.FunctionalTests.FunctionalTests.Composants;

using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using ApiCartobani.FunctionalTests.TestUtilities;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteComposantTests : TestBase
{
    [Test]
    public async Task delete_composant_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeComposant = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.TypeComposant, _ => fakeTypeElementOne.Id).Generate());
        await InsertAsync(fakeComposant);

        // Act
        var route = ApiRoutes.Composants.Delete.Replace(ApiRoutes.Composants.Id, fakeComposant.Id.ToString());
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}