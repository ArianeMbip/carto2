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

public class GetFluxTests : TestBase
{
    [Test]
    public async Task get_flux_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeFlux = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Entree, _ => fakeActifOne.Id)
            .RuleFor(f => f.Sortie, _ => fakeActifOne.Id)
            .RuleFor(f => f.TypeFlux, _ => fakeTypeElementOne.Id).Generate());
        await InsertAsync(fakeFlux);

        // Act
        var route = ApiRoutes.Flux.GetRecord.Replace(ApiRoutes.Flux.Id, fakeFlux.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}