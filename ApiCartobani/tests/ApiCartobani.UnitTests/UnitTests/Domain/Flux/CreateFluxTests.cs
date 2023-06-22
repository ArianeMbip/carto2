namespace ApiCartobani.UnitTests.UnitTests.Domain.Flux;

using ApiCartobani.SharedTestHelpers.Fakes.Flux;
using ApiCartobani.Domain.Flux;
using ApiCartobani.Domain.Flux.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateFluxTests
{
    private readonly Faker _faker;

    public CreateFluxTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_flux()
    {
        // Arrange
        var fluxToCreate = new FakeFluxForCreationDto().Generate();
        
        // Act
        var fakeFlux = Flux.Create(fluxToCreate);

        // Assert
        fakeFlux.Nom.Should().Be(fluxToCreate.Nom);
        fakeFlux.Entree.Should().Be(fluxToCreate.Entree);
        fakeFlux.Sortie.Should().Be(fluxToCreate.Sortie);
        fakeFlux.Description.Should().Be(fluxToCreate.Description);
        fakeFlux.TypeFlux.Should().Be(fluxToCreate.TypeFlux);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeFlux = FakeFlux.Generate();

        // Assert
        fakeFlux.DomainEvents.Count.Should().Be(1);
        fakeFlux.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(FluxCreated));
    }
}