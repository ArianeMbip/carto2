namespace ApiCartobani.IntegrationTests.FeatureTests.DAs;

using ApiCartobani.SharedTestHelpers.Fakes.DA;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.DAs.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;

public class AddDACommandTests : TestBase
{
    [Test]
    public async Task can_add_new_da_to_db()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeDAOne = new FakeDAForCreationDto()
            .RuleFor(d => d.IdActif, _ => fakeActifOne.Id).Generate();

        // Act
        var command = new AddDA.Command(fakeDAOne);
        var dAReturned = await SendAsync(command);
        var dACreated = await ExecuteDbContextAsync(db => db.DAs
            .FirstOrDefaultAsync(d => d.Id == dAReturned.Id));

        // Assert
        dAReturned.Contexte.Should().Be(fakeDAOne.Contexte);
        dAReturned.Objectifs.Should().Be(fakeDAOne.Objectifs);
        dAReturned.Status.Should().Be(fakeDAOne.Status);
        dAReturned.DomaineFonctionnel.Should().Be(fakeDAOne.DomaineFonctionnel);
        dAReturned.SousDomaineFonctionnel.Should().Be(fakeDAOne.SousDomaineFonctionnel);
        dAReturned.Fonction                    .Should().Be(fakeDAOne.Fonction                    );
        dAReturned.Acteurs.Should().Be(fakeDAOne.Acteurs);
        dAReturned.CasUtilisation.Should().Be(fakeDAOne.CasUtilisation);
        dAReturned.DiagrammeSequence.Should().Be(fakeDAOne.DiagrammeSequence);
        dAReturned.ArchitectureFonctionnelle.Should().Be(fakeDAOne.ArchitectureFonctionnelle);
        dAReturned.ArchitectureTechnique.Should().Be(fakeDAOne.ArchitectureTechnique);
        dAReturned.ArchitectureApplicative.Should().Be(fakeDAOne.ArchitectureApplicative);
        dAReturned.IdActif.Should().Be(fakeDAOne.IdActif);
        dAReturned.ArchitectureDonnee.Should().Be(fakeDAOne.ArchitectureDonnee);
        dAReturned.Property21.Should().Be(fakeDAOne.Property21);

        dACreated.Contexte.Should().Be(fakeDAOne.Contexte);
        dACreated.Objectifs.Should().Be(fakeDAOne.Objectifs);
        dACreated.Status.Should().Be(fakeDAOne.Status);
        dACreated.DomaineFonctionnel.Should().Be(fakeDAOne.DomaineFonctionnel);
        dACreated.SousDomaineFonctionnel.Should().Be(fakeDAOne.SousDomaineFonctionnel);
        dACreated.Fonction                    .Should().Be(fakeDAOne.Fonction                    );
        dACreated.Acteurs.Should().Be(fakeDAOne.Acteurs);
        dACreated.CasUtilisation.Should().Be(fakeDAOne.CasUtilisation);
        dACreated.DiagrammeSequence.Should().Be(fakeDAOne.DiagrammeSequence);
        dACreated.ArchitectureFonctionnelle.Should().Be(fakeDAOne.ArchitectureFonctionnelle);
        dACreated.ArchitectureTechnique.Should().Be(fakeDAOne.ArchitectureTechnique);
        dACreated.ArchitectureApplicative.Should().Be(fakeDAOne.ArchitectureApplicative);
        dACreated.IdActif.Should().Be(fakeDAOne.IdActif);
        dACreated.ArchitectureDonnee.Should().Be(fakeDAOne.ArchitectureDonnee);
        dACreated.Property21.Should().Be(fakeDAOne.Property21);
    }
}