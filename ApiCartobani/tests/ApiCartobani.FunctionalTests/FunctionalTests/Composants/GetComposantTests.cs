namespace ApiCartobani.FunctionalTests.FunctionalTests.Composants;

using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using ApiCartobani.FunctionalTests.TestUtilities;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetComposantTests : TestBase
{
    [Test]
    public async Task get_composant_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeComposant = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.TypeComposant, _ => fakeTypeElementOne.Id).Generate());
        await InsertAsync(fakeComposant);

        // Act
        var route = ApiRoutes.Composants.GetRecord.Replace(ApiRoutes.Composants.Id, fakeComposant.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}