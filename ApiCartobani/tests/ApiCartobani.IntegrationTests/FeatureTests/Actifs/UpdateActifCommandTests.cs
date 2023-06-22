namespace ApiCartobani.IntegrationTests.FeatureTests.Actifs;

using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.Domain.Actifs.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Actifs.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;

public class UpdateActifCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_actif_in_db()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

        fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.TypeActif, _ => fakeTypeElementOne.Id)
            .RuleFor(a => a.PreVersion, _ => fakeActifParentOne.Id)
            .RuleFor(a => a.Hierarchie, _ => fakeActifParentOne.Id).Generate());
        var updatedActifDto = new FakeActifForUpdateDto()
            .RuleFor(a => a.TypeActif, _ => fakeTypeElementOne.Id)
            .RuleFor(a => a.PreVersion, _ => fakeActifParentOne.Id)
            .RuleFor(a => a.Hierarchie, _ => fakeActifParentOne.Id).Generate();
        await InsertAsync(fakeActifOne);

        var actif = await ExecuteDbContextAsync(db => db.Actifs
            .FirstOrDefaultAsync(a => a.Id == fakeActifOne.Id));
        var id = actif.Id;

        // Act
        var command = new UpdateActif.Command(id, updatedActifDto);
        await SendAsync(command);
        var updatedActif = await ExecuteDbContextAsync(db => db.Actifs.FirstOrDefaultAsync(a => a.Id == id));

        // Assert
        updatedActif.Nom.Should().Be(updatedActifDto.Nom);
        updatedActif.Criticite.Should().Be(updatedActifDto.Criticite);
        updatedActif.Description.Should().Be(updatedActifDto.Description);
        updatedActif.Version.Should().Be(updatedActifDto.Version);
        updatedActif.Icone.Should().Be(updatedActifDto.Icone);
        updatedActif.Statut.Should().Be(updatedActifDto.Statut);
        updatedActif.TypeActif.Should().Be(updatedActifDto.TypeActif);
        updatedActif.PreVersion.Should().Be(updatedActifDto.PreVersion);
        updatedActif.Hierarchie.Should().Be(updatedActifDto.Hierarchie);
    }
}