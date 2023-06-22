namespace ApiCartobani.UnitTests.UnitTests.Domain.DAs;

using ApiCartobani.SharedTestHelpers.Fakes.DA;
using ApiCartobani.Domain.DAs;
using ApiCartobani.Domain.DAs.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateDATests
{
    private readonly Faker _faker;

    public CreateDATests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_dA()
    {
        // Arrange
        var dAToCreate = new FakeDAForCreationDto().Generate();
        
        // Act
        var fakeDA = DA.Create(dAToCreate);

        // Assert
        fakeDA.Contexte.Should().Be(dAToCreate.Contexte);
        fakeDA.Objectifs.Should().Be(dAToCreate.Objectifs);
        fakeDA.Status.Should().Be(dAToCreate.Status);
        fakeDA.DomaineFonctionnel.Should().Be(dAToCreate.DomaineFonctionnel);
        fakeDA.SousDomaineFonctionnel.Should().Be(dAToCreate.SousDomaineFonctionnel);
        fakeDA.Fonction                    .Should().Be(dAToCreate.Fonction                    );
        fakeDA.Acteurs.Should().Be(dAToCreate.Acteurs);
        fakeDA.CasUtilisation.Should().Be(dAToCreate.CasUtilisation);
        fakeDA.DiagrammeSequence.Should().Be(dAToCreate.DiagrammeSequence);
        fakeDA.ArchitectureFonctionnelle.Should().Be(dAToCreate.ArchitectureFonctionnelle);
        fakeDA.ArchitectureTechnique.Should().Be(dAToCreate.ArchitectureTechnique);
        fakeDA.ArchitectureApplicative.Should().Be(dAToCreate.ArchitectureApplicative);
        fakeDA.IdActif.Should().Be(dAToCreate.IdActif);
        fakeDA.ArchitectureDonnee.Should().Be(dAToCreate.ArchitectureDonnee);
        fakeDA.Property21.Should().Be(dAToCreate.Property21);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeDA = FakeDA.Generate();

        // Assert
        fakeDA.DomainEvents.Count.Should().Be(1);
        fakeDA.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(DACreated));
    }
}