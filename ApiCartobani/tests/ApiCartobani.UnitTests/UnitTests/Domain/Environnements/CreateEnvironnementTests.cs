namespace ApiCartobani.UnitTests.UnitTests.Domain.Environnements;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using ApiCartobani.Domain.Environnements;
using ApiCartobani.Domain.Environnements.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateEnvironnementTests
{
    private readonly Faker _faker;

    public CreateEnvironnementTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_environnement()
    {
        // Arrange
        var environnementToCreate = new FakeEnvironnementForCreationDto().Generate();
        
        // Act
        var fakeEnvironnement = Environnement.Create(environnementToCreate);

        // Assert
        fakeEnvironnement.Nom.Should().Be(environnementToCreate.Nom);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeEnvironnement = FakeEnvironnement.Generate();

        // Assert
        fakeEnvironnement.DomainEvents.Count.Should().Be(1);
        fakeEnvironnement.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(EnvironnementCreated));
    }
}