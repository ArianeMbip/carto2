namespace ApiCartobani.IntegrationTests.FeatureTests.DAs;

using ApiCartobani.SharedTestHelpers.Fakes.DA;
using ApiCartobani.Domain.DAs.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.DAs.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;

public class UpdateDACommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_da_in_db()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeDAOne = FakeDA.Generate(new FakeDAForCreationDto()
            .RuleFor(d => d.IdActif, _ => fakeActifOne.Id).Generate());
        var updatedDADto = new FakeDAForUpdateDto()
            .RuleFor(d => d.IdActif, _ => fakeActifOne.Id).Generate();
        await InsertAsync(fakeDAOne);

        var dA = await ExecuteDbContextAsync(db => db.DAs
            .FirstOrDefaultAsync(d => d.Id == fakeDAOne.Id));
        var id = dA.Id;

        // Act
        var command = new UpdateDA.Command(id, updatedDADto);
        await SendAsync(command);
        var updatedDA = await ExecuteDbContextAsync(db => db.DAs.FirstOrDefaultAsync(d => d.Id == id));

        // Assert
        updatedDA.Contexte.Should().Be(updatedDADto.Contexte);
        updatedDA.Objectifs.Should().Be(updatedDADto.Objectifs);
        updatedDA.Status.Should().Be(updatedDADto.Status);
        updatedDA.DomaineFonctionnel.Should().Be(updatedDADto.DomaineFonctionnel);
        updatedDA.SousDomaineFonctionnel.Should().Be(updatedDADto.SousDomaineFonctionnel);
        updatedDA.Fonction                    .Should().Be(updatedDADto.Fonction                    );
        updatedDA.Acteurs.Should().Be(updatedDADto.Acteurs);
        updatedDA.CasUtilisation.Should().Be(updatedDADto.CasUtilisation);
        updatedDA.DiagrammeSequence.Should().Be(updatedDADto.DiagrammeSequence);
        updatedDA.ArchitectureFonctionnelle.Should().Be(updatedDADto.ArchitectureFonctionnelle);
        updatedDA.ArchitectureTechnique.Should().Be(updatedDADto.ArchitectureTechnique);
        updatedDA.ArchitectureApplicative.Should().Be(updatedDADto.ArchitectureApplicative);
        updatedDA.IdActif.Should().Be(updatedDADto.IdActif);
        updatedDA.ArchitectureDonnee.Should().Be(updatedDADto.ArchitectureDonnee);
        updatedDA.Property21.Should().Be(updatedDADto.Property21);
    }
}