namespace ApiCartobani.UnitTests.UnitTests.Domain.Fonctionnalites;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using ApiCartobani.Domain.Fonctionnalites;
using ApiCartobani.Domain.Fonctionnalites.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateFonctionnaliteTests
{
    private readonly Faker _faker;

    public CreateFonctionnaliteTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_fonctionnalite()
    {
        // Arrange
        var fonctionnaliteToCreate = new FakeFonctionnaliteForCreationDto().Generate();
        
        // Act
        var fakeFonctionnalite = Fonctionnalite.Create(fonctionnaliteToCreate);

        // Assert
        fakeFonctionnalite.Nom.Should().Be(fonctionnaliteToCreate.Nom);
        fakeFonctionnalite.Type.Should().Be(fonctionnaliteToCreate.Type);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeFonctionnalite = FakeFonctionnalite.Generate();

        // Assert
        fakeFonctionnalite.DomainEvents.Count.Should().Be(1);
        fakeFonctionnalite.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(FonctionnaliteCreated));
    }
}