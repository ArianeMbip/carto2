namespace ApiCartobani.IntegrationTests.FeatureTests.Actifs;

using ApiCartobani.Domain.Actifs.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Actifs.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;

public class ActifListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_actif_list()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        var fakeTypeElementTwo = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne, fakeTypeElementTwo);

        var fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        var fakeActifParentTwo = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne, fakeActifParentTwo);

         fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
         fakeActifParentTwo = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne, fakeActifParentTwo);

        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.TypeActif, _ => fakeTypeElementOne.Id)
            .RuleFor(a => a.PreVersion, _ => fakeActifParentOne.Id)
            .RuleFor(a => a.Hierarchie, _ => fakeActifParentOne.Id).Generate());
        var fakeActifTwo = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.TypeActif, _ => fakeTypeElementTwo.Id)
            .RuleFor(a => a.PreVersion, _ => fakeActifParentTwo.Id)
            .RuleFor(a => a.Hierarchie, _ => fakeActifParentTwo.Id).Generate());
        var queryParameters = new ActifParametersDto();

        await InsertAsync(fakeActifOne, fakeActifTwo);

        // Act
        var query = new GetActifList.Query(queryParameters);
        var actifs = await SendAsync(query);

        // Assert
        actifs.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}