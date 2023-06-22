namespace ApiCartobani.UnitTests.UnitTests.Domain.Attributs;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.Domain.Attributs;
using ApiCartobani.Domain.Attributs.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateAttributTests
{
    private readonly Faker _faker;

    public UpdateAttributTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_attribut()
    {
        // Arrange
        var fakeAttribut = FakeAttribut.Generate();
        var updatedAttribut = new FakeAttributForUpdateDto().Generate();
        
        // Act
        fakeAttribut.Update(updatedAttribut);

        // Assert
        fakeAttribut.Nom.Should().Be(updatedAttribut.Nom);
        fakeAttribut.Requis.Should().Be(updatedAttribut.Requis);
        fakeAttribut.TypeDonnee.Should().Be(updatedAttribut.TypeDonnee);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeAttribut = FakeAttribut.Generate();
        var updatedAttribut = new FakeAttributForUpdateDto().Generate();
        fakeAttribut.DomainEvents.Clear();
        
        // Act
        fakeAttribut.Update(updatedAttribut);

        // Assert
        fakeAttribut.DomainEvents.Count.Should().Be(1);
        fakeAttribut.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(AttributUpdated));
    }
}