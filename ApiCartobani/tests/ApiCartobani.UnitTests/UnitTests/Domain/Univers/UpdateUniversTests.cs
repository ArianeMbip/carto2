namespace ApiCartobani.UnitTests.UnitTests.Domain.Univers;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using ApiCartobani.Domain.Univers;
using ApiCartobani.Domain.Univers.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateUniversTests
{
    private readonly Faker _faker;

    public UpdateUniversTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_univers()
    {
        // Arrange
        var fakeUnivers = FakeUnivers.Generate();
        var updatedUnivers = new FakeUniversForUpdateDto().Generate();
        
        // Act
        fakeUnivers.Update(updatedUnivers);

        // Assert
        fakeUnivers.Nom.Should().Be(updatedUnivers.Nom);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeUnivers = FakeUnivers.Generate();
        var updatedUnivers = new FakeUniversForUpdateDto().Generate();
        fakeUnivers.DomainEvents.Clear();
        
        // Act
        fakeUnivers.Update(updatedUnivers);

        // Assert
        fakeUnivers.DomainEvents.Count.Should().Be(1);
        fakeUnivers.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UniversUpdated));
    }
}