namespace ApiCartobani.IntegrationTests.FeatureTests.Composants;

using ApiCartobani.Domain.Composants.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Composants.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

public class ComposantListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_composant_list()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        var fakeTypeElementTwo = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne, fakeTypeElementTwo);

        var fakeComposantOne = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.TypeComposant, _ => fakeTypeElementOne.Id).Generate());
        var fakeComposantTwo = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.TypeComposant, _ => fakeTypeElementTwo.Id).Generate());
        var queryParameters = new ComposantParametersDto();

        await InsertAsync(fakeComposantOne, fakeComposantTwo);

        // Act
        var query = new GetComposantList.Query(queryParameters);
        var composants = await SendAsync(query);

        // Assert
        composants.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}