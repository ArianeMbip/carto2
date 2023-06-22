namespace ApiCartobani.IntegrationTests.FeatureTests.Flux;

using ApiCartobani.SharedTestHelpers.Fakes.Flux;
using ApiCartobani.Domain.Flux.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

public class FluxQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_flux_with_accurate_props()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

         fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeFluxOne = FakeFlux.Generate(new FakeFluxForCreationDto()
            .RuleFor(f => f.Entree, _ => fakeActifOne.Id)
            .RuleFor(f => f.Sortie, _ => fakeActifOne.Id)
            .RuleFor(f => f.TypeFlux, _ => fakeTypeElementOne.Id).Generate());
        await InsertAsync(fakeFluxOne);

        // Act
        var query = new GetFlux.Query(fakeFluxOne.Id);
        var flux = await SendAsync(query);

        // Assert
        flux.Nom.Should().Be(fakeFluxOne.Nom);
        flux.Entree.Should().Be(fakeFluxOne.Entree);
        flux.Sortie.Should().Be(fakeFluxOne.Sortie);
        flux.Description.Should().Be(fakeFluxOne.Description);
        flux.TypeFlux.Should().Be(fakeFluxOne.TypeFlux);
    }

    [Test]
    public async Task get_flux_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetFlux.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}