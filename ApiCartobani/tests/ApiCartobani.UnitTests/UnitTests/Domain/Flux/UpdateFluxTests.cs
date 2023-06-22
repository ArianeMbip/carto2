namespace ApiCartobani.UnitTests.UnitTests.Domain.Flux;

using ApiCartobani.SharedTestHelpers.Fakes.Flux;
using ApiCartobani.Domain.Flux;
using ApiCartobani.Domain.Flux.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateFluxTests
{
    private readonly Faker _faker;

    public UpdateFluxTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_flux()
    {
        // Arrange
        var fakeFlux = FakeFlux.Generate();
        var updatedFlux = new FakeFluxForUpdateDto().Generate();
        
        // Act
        fakeFlux.Update(updatedFlux);

        // Assert
        fakeFlux.Nom.Should().Be(updatedFlux.Nom);
        fakeFlux.Entree.Should().Be(updatedFlux.Entree);
        fakeFlux.Sortie.Should().Be(updatedFlux.Sortie);
        fakeFlux.Description.Should().Be(updatedFlux.Description);
        fakeFlux.TypeFlux.Should().Be(updatedFlux.TypeFlux);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeFlux = FakeFlux.Generate();
        var updatedFlux = new FakeFluxForUpdateDto().Generate();
        fakeFlux.DomainEvents.Clear();
        
        // Act
        fakeFlux.Update(updatedFlux);

        // Assert
        fakeFlux.DomainEvents.Count.Should().Be(1);
        fakeFlux.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(FluxUpdated));
    }
}