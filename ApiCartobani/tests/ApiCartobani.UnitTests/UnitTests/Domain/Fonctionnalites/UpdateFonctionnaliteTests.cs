namespace ApiCartobani.UnitTests.UnitTests.Domain.Fonctionnalites;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using ApiCartobani.Domain.Fonctionnalites;
using ApiCartobani.Domain.Fonctionnalites.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateFonctionnaliteTests
{
    private readonly Faker _faker;

    public UpdateFonctionnaliteTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_fonctionnalite()
    {
        // Arrange
        var fakeFonctionnalite = FakeFonctionnalite.Generate();
        var updatedFonctionnalite = new FakeFonctionnaliteForUpdateDto().Generate();
        
        // Act
        fakeFonctionnalite.Update(updatedFonctionnalite);

        // Assert
        fakeFonctionnalite.Nom.Should().Be(updatedFonctionnalite.Nom);
        fakeFonctionnalite.Type.Should().Be(updatedFonctionnalite.Type);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeFonctionnalite = FakeFonctionnalite.Generate();
        var updatedFonctionnalite = new FakeFonctionnaliteForUpdateDto().Generate();
        fakeFonctionnalite.DomainEvents.Clear();
        
        // Act
        fakeFonctionnalite.Update(updatedFonctionnalite);

        // Assert
        fakeFonctionnalite.DomainEvents.Count.Should().Be(1);
        fakeFonctionnalite.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(FonctionnaliteUpdated));
    }
}