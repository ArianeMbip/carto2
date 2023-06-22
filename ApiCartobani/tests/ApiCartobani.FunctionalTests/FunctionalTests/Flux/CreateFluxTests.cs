namespace ApiCartobani.FunctionalTests.FunctionalTests.Flux;

using ApiCartobani.SharedTestHelpers.Fakes.Flux;
using ApiCartobani.FunctionalTests.TestUtilities;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateFluxTests : TestBase
{
    [Test]
    public async Task create_flux_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeFlux = new FakeFluxForCreationDto()
            .RuleFor(f => f.Entree, _ => fakeActifOne.Id)
            
            .RuleFor(f => f.Sortie, _ => fakeActifOne.Id)
            
            .RuleFor(f => f.TypeFlux, _ => fakeTypeElementOne.Id)
            .Generate();

        // Act
        var route = ApiRoutes.Flux.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeFlux);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}