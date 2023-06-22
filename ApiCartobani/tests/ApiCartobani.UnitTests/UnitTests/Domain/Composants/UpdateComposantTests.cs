namespace ApiCartobani.UnitTests.UnitTests.Domain.Composants;

using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using ApiCartobani.Domain.Composants;
using ApiCartobani.Domain.Composants.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateComposantTests
{
    private readonly Faker _faker;

    public UpdateComposantTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_composant()
    {
        // Arrange
        var fakeComposant = FakeComposant.Generate();
        var updatedComposant = new FakeComposantForUpdateDto().Generate();
        
        // Act
        fakeComposant.Update(updatedComposant);

        // Assert
        fakeComposant.Nom.Should().Be(updatedComposant.Nom);
        fakeComposant.TypeComposant.Should().Be(updatedComposant.TypeComposant);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeComposant = FakeComposant.Generate();
        var updatedComposant = new FakeComposantForUpdateDto().Generate();
        fakeComposant.DomainEvents.Clear();
        
        // Act
        fakeComposant.Update(updatedComposant);

        // Assert
        fakeComposant.DomainEvents.Count.Should().Be(1);
        fakeComposant.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ComposantUpdated));
    }
}