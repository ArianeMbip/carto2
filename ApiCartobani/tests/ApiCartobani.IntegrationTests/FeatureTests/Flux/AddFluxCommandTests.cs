namespace ApiCartobani.IntegrationTests.FeatureTests.Flux;

using ApiCartobani.SharedTestHelpers.Fakes.Flux;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.Flux.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

public class AddFluxCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_flux_to_db()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeFluxOne = new FakeFluxForCreationDto()
            .RuleFor(f => f.Entree, _ => fakeActifOne.Id)
            .RuleFor(f => f.Sortie, _ => fakeActifOne.Id)
            .RuleFor(f => f.TypeFlux, _ => fakeTypeElementOne.Id).Generate();

        // Act
        var command = new AddFlux.Command(fakeFluxOne);
        var fluxReturned = await SendAsync(command);
        var fluxCreated = await ExecuteDbContextAsync(db => db.Flux
            .FirstOrDefaultAsync(f => f.Id == fluxReturned.Id));

        // Assert
        fluxReturned.Nom.Should().Be(fakeFluxOne.Nom);
        fluxReturned.Entree.Should().Be(fakeFluxOne.Entree);
        fluxReturned.Sortie.Should().Be(fakeFluxOne.Sortie);
        fluxReturned.Description.Should().Be(fakeFluxOne.Description);
        fluxReturned.TypeFlux.Should().Be(fakeFluxOne.TypeFlux);

        fluxCreated.Nom.Should().Be(fakeFluxOne.Nom);
        fluxCreated.Entree.Should().Be(fakeFluxOne.Entree);
        fluxCreated.Sortie.Should().Be(fakeFluxOne.Sortie);
        fluxCreated.Description.Should().Be(fakeFluxOne.Description);
        fluxCreated.TypeFlux.Should().Be(fakeFluxOne.TypeFlux);
    }
}