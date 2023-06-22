namespace ApiCartobani.IntegrationTests.FeatureTests.Flux;

using ApiCartobani.Domain.Flux.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.Flux;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Flux.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

public class FluxListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_flux_list()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        var fakeActifTwo = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne, fakeActifTwo);

         fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
         fakeActifTwo = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne, fakeActifTwo);

        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        var fakeTypeElementTwo = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne, fakeTypeElementTwo);

        var fakeFluxOne = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Entree, _ => fakeActifOne.Id)
            .RuleFor(f => f.Sortie, _ => fakeActifOne.Id)
            .RuleFor(f => f.TypeFlux, _ => fakeTypeElementOne.Id).Generate());
        var fakeFluxTwo = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Entree, _ => fakeActifTwo.Id)
            .RuleFor(f => f.Sortie, _ => fakeActifTwo.Id)
            .RuleFor(f => f.TypeFlux, _ => fakeTypeElementTwo.Id).Generate());
        var queryParameters = new FluxParametersDto();

        await InsertAsync(fakeFluxOne, fakeFluxTwo);

        // Act
        var query = new GetFluxList.Query(queryParameters);
        var flux = await SendAsync(query);

        // Assert
        flux.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}