namespace ApiCartobani.UnitTests.UnitTests.Domain.ValeurAttributs;

using ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;
using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.ValeurAttributs.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateValeurAttributTests
{
    private readonly Faker _faker;

    public UpdateValeurAttributTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_valeurAttribut()
    {
        // Arrange
        var fakeValeurAttribut = FakeValeurAttribut.Generate();
        var updatedValeurAttribut = new FakeValeurAttributForUpdateDto().Generate();
        
        // Act
        fakeValeurAttribut.Update(updatedValeurAttribut);

        // Assert
        fakeValeurAttribut.Valeur.Should().Be(updatedValeurAttribut.Valeur);
        fakeValeurAttribut.Attribut.Should().Be(updatedValeurAttribut.Attribut);
        fakeValeurAttribut.Environnement.Should().Be(updatedValeurAttribut.Environnement);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeValeurAttribut = FakeValeurAttribut.Generate();
        var updatedValeurAttribut = new FakeValeurAttributForUpdateDto().Generate();
        fakeValeurAttribut.DomainEvents.Clear();
        
        // Act
        fakeValeurAttribut.Update(updatedValeurAttribut);

        // Assert
        fakeValeurAttribut.DomainEvents.Count.Should().Be(1);
        fakeValeurAttribut.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ValeurAttributUpdated));
    }
}