namespace ApiCartobani.IntegrationTests.FeatureTests.DAs;

using ApiCartobani.SharedTestHelpers.Fakes.DA;
using ApiCartobani.Domain.DAs.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;

public class DAQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_da_with_accurate_props()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeDAOne = FakeDA.Generate(new FakeDAForCreationDto()
            .RuleFor(d => d.IdActif, _ => fakeActifOne.Id).Generate());
        await InsertAsync(fakeDAOne);

        // Act
        var query = new GetDA.Query(fakeDAOne.Id);
        var dA = await SendAsync(query);

        // Assert
        dA.Contexte.Should().Be(fakeDAOne.Contexte);
        dA.Objectifs.Should().Be(fakeDAOne.Objectifs);
        dA.Status.Should().Be(fakeDAOne.Status);
        dA.DomaineFonctionnel.Should().Be(fakeDAOne.DomaineFonctionnel);
        dA.SousDomaineFonctionnel.Should().Be(fakeDAOne.SousDomaineFonctionnel);
        dA.Fonction                    .Should().Be(fakeDAOne.Fonction                    );
        dA.Acteurs.Should().Be(fakeDAOne.Acteurs);
        dA.CasUtilisation.Should().Be(fakeDAOne.CasUtilisation);
        dA.DiagrammeSequence.Should().Be(fakeDAOne.DiagrammeSequence);
        dA.ArchitectureFonctionnelle.Should().Be(fakeDAOne.ArchitectureFonctionnelle);
        dA.ArchitectureTechnique.Should().Be(fakeDAOne.ArchitectureTechnique);
        dA.ArchitectureApplicative.Should().Be(fakeDAOne.ArchitectureApplicative);
        dA.IdActif.Should().Be(fakeDAOne.IdActif);
        dA.ArchitectureDonnee.Should().Be(fakeDAOne.ArchitectureDonnee);
        dA.Property21.Should().Be(fakeDAOne.Property21);
    }

    [Test]
    public async Task get_da_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetDA.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}