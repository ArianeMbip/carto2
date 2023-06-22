namespace ApiCartobani.IntegrationTests.FeatureTests.Flux;

using ApiCartobani.SharedTestHelpers.Fakes.Flux;
using ApiCartobani.Domain.Flux.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

public class DeleteFluxCommandTests : TestBase
{
    [Test]
    public async Task can_delete_flux_from_db()
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
        var flux = await ExecuteDbContextAsync(db => db.Flux
            .FirstOrDefaultAsync(f => f.Id == fakeFluxOne.Id));

        // Act
        var command = new DeleteFlux.Command(flux.Id);
        await SendAsync(command);
        var fluxResponse = await ExecuteDbContextAsync(db => db.Flux.CountAsync(f => f.Id == flux.Id));

        // Assert
        fluxResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_flux_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteFlux.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_flux_from_db()
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
        var flux = await ExecuteDbContextAsync(db => db.Flux
            .FirstOrDefaultAsync(f => f.Id == fakeFluxOne.Id));

        // Act
        var command = new DeleteFlux.Command(flux.Id);
        await SendAsync(command);
        var deletedFlux = await ExecuteDbContextAsync(db => db.Flux
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == flux.Id));

        // Assert
        deletedFlux?.IsDeleted.Should().BeTrue();
    }
}