namespace ApiCartobani.UnitTests.UnitTests.Domain.Icones;

using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using ApiCartobani.Domain.Icones;
using ApiCartobani.Domain.Icones.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateIconeTests
{
    private readonly Faker _faker;

    public UpdateIconeTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_icone()
    {
        // Arrange
        var fakeIcone = FakeIcone.Generate();
        var updatedIcone = new FakeIconeForUpdateDto().Generate();
        
        // Act
        fakeIcone.Update(updatedIcone);

        // Assert
        fakeIcone.Url.Should().Be(updatedIcone.Url);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeIcone = FakeIcone.Generate();
        var updatedIcone = new FakeIconeForUpdateDto().Generate();
        fakeIcone.DomainEvents.Clear();
        
        // Act
        fakeIcone.Update(updatedIcone);

        // Assert
        fakeIcone.DomainEvents.Count.Should().Be(1);
        fakeIcone.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(IconeUpdated));
    }
}