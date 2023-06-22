namespace ApiCartobani.UnitTests.UnitTests.Domain.Composants;

using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using ApiCartobani.Domain.Composants;
using ApiCartobani.Domain.Composants.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateComposantTests
{
    private readonly Faker _faker;

    public CreateComposantTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_composant()
    {
        // Arrange
        var composantToCreate = new FakeComposantForCreationDto().Generate();
        
        // Act
        var fakeComposant = Composant.Create(composantToCreate);

        // Assert
        fakeComposant.Nom.Should().Be(composantToCreate.Nom);
        fakeComposant.TypeComposant.Should().Be(composantToCreate.TypeComposant);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeComposant = FakeComposant.Generate();

        // Assert
        fakeComposant.DomainEvents.Count.Should().Be(1);
        fakeComposant.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ComposantCreated));
    }
}