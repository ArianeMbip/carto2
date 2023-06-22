namespace ApiCartobani.UnitTests.UnitTests.Domain.Environnements;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using ApiCartobani.Domain.Environnements;
using ApiCartobani.Domain.Environnements.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateEnvironnementTests
{
    private readonly Faker _faker;

    public UpdateEnvironnementTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_environnement()
    {
        // Arrange
        var fakeEnvironnement = FakeEnvironnement.Generate();
        var updatedEnvironnement = new FakeEnvironnementForUpdateDto().Generate();
        
        // Act
        fakeEnvironnement.Update(updatedEnvironnement);

        // Assert
        fakeEnvironnement.Nom.Should().Be(updatedEnvironnement.Nom);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeEnvironnement = FakeEnvironnement.Generate();
        var updatedEnvironnement = new FakeEnvironnementForUpdateDto().Generate();
        fakeEnvironnement.DomainEvents.Clear();
        
        // Act
        fakeEnvironnement.Update(updatedEnvironnement);

        // Assert
        fakeEnvironnement.DomainEvents.Count.Should().Be(1);
        fakeEnvironnement.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(EnvironnementUpdated));
    }
}