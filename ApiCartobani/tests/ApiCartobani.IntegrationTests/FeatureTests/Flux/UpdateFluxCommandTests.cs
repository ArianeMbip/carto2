namespace ApiCartobani.IntegrationTests.FeatureTests.Flux;

using ApiCartobani.SharedTestHelpers.Fakes.Flux;
using ApiCartobani.Domain.Flux.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Flux.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

public class UpdateFluxCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_flux_in_db()
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
        var updatedFluxDto = new FakeFluxForUpdateDto()
            .RuleFor(f => f.Entree, _ => fakeActifOne.Id)
            .RuleFor(f => f.Sortie, _ => fakeActifOne.Id)
            .RuleFor(f => f.TypeFlux, _ => fakeTypeElementOne.Id).Generate();
        await InsertAsync(fakeFluxOne);

        var flux = await ExecuteDbContextAsync(db => db.Flux
            .FirstOrDefaultAsync(f => f.Id == fakeFluxOne.Id));
        var id = flux.Id;

        // Act
        var command = new UpdateFlux.Command(id, updatedFluxDto);
        await SendAsync(command);
        var updatedFlux = await ExecuteDbContextAsync(db => db.Flux.FirstOrDefaultAsync(f => f.Id == id));

        // Assert
        updatedFlux.Nom.Should().Be(updatedFluxDto.Nom);
        updatedFlux.Entree.Should().Be(updatedFluxDto.Entree);
        updatedFlux.Sortie.Should().Be(updatedFluxDto.Sortie);
        updatedFlux.Description.Should().Be(updatedFluxDto.Description);
        updatedFlux.TypeFlux.Should().Be(updatedFluxDto.TypeFlux);
    }
}