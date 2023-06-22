namespace ApiCartobani.UnitTests.UnitTests.Domain.ValeurAttributs;

using ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;
using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.ValeurAttributs.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateValeurAttributTests
{
    private readonly Faker _faker;

    public CreateValeurAttributTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_valeurAttribut()
    {
        // Arrange
        var valeurAttributToCreate = new FakeValeurAttributForCreationDto().Generate();
        
        // Act
        var fakeValeurAttribut = ValeurAttribut.Create(valeurAttributToCreate);

        // Assert
        fakeValeurAttribut.Valeur.Should().Be(valeurAttributToCreate.Valeur);
        fakeValeurAttribut.Attribut.Should().Be(valeurAttributToCreate.Attribut);
        fakeValeurAttribut.Environnement.Should().Be(valeurAttributToCreate.Environnement);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeValeurAttribut = FakeValeurAttribut.Generate();

        // Assert
        fakeValeurAttribut.DomainEvents.Count.Should().Be(1);
        fakeValeurAttribut.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(ValeurAttributCreated));
    }
}