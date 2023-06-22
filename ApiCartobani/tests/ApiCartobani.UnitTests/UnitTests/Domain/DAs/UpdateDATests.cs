namespace ApiCartobani.UnitTests.UnitTests.Domain.DAs;

using ApiCartobani.SharedTestHelpers.Fakes.DA;
using ApiCartobani.Domain.DAs;
using ApiCartobani.Domain.DAs.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateDATests
{
    private readonly Faker _faker;

    public UpdateDATests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_dA()
    {
        // Arrange
        var fakeDA = FakeDA.Generate();
        var updatedDA = new FakeDAForUpdateDto().Generate();
        
        // Act
        fakeDA.Update(updatedDA);

        // Assert
        fakeDA.Contexte.Should().Be(updatedDA.Contexte);
        fakeDA.Objectifs.Should().Be(updatedDA.Objectifs);
        fakeDA.Status.Should().Be(updatedDA.Status);
        fakeDA.DomaineFonctionnel.Should().Be(updatedDA.DomaineFonctionnel);
        fakeDA.SousDomaineFonctionnel.Should().Be(updatedDA.SousDomaineFonctionnel);
        fakeDA.Fonction                    .Should().Be(updatedDA.Fonction                    );
        fakeDA.Acteurs.Should().Be(updatedDA.Acteurs);
        fakeDA.CasUtilisation.Should().Be(updatedDA.CasUtilisation);
        fakeDA.DiagrammeSequence.Should().Be(updatedDA.DiagrammeSequence);
        fakeDA.ArchitectureFonctionnelle.Should().Be(updatedDA.ArchitectureFonctionnelle);
        fakeDA.ArchitectureTechnique.Should().Be(updatedDA.ArchitectureTechnique);
        fakeDA.ArchitectureApplicative.Should().Be(updatedDA.ArchitectureApplicative);
        fakeDA.IdActif.Should().Be(updatedDA.IdActif);
        fakeDA.ArchitectureDonnee.Should().Be(updatedDA.ArchitectureDonnee);
        fakeDA.Property21.Should().Be(updatedDA.Property21);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeDA = FakeDA.Generate();
        var updatedDA = new FakeDAForUpdateDto().Generate();
        fakeDA.DomainEvents.Clear();
        
        // Act
        fakeDA.Update(updatedDA);

        // Assert
        fakeDA.DomainEvents.Count.Should().Be(1);
        fakeDA.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(DAUpdated));
    }
}