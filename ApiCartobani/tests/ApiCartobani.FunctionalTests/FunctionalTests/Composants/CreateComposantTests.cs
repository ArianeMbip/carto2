namespace ApiCartobani.FunctionalTests.FunctionalTests.Composants;

using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using ApiCartobani.FunctionalTests.TestUtilities;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateComposantTests : TestBase
{
    [Test]
    public async Task create_composant_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeComposant = new FakeComposantForCreationDto()
            .RuleFor(c => c.TypeComposant, _ => fakeTypeElementOne.Id)
            .Generate();

        // Act
        var route = ApiRoutes.Composants.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeComposant);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}