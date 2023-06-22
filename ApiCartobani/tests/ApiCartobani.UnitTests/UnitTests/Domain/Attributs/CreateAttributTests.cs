namespace ApiCartobani.UnitTests.UnitTests.Domain.Attributs;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.Domain.Attributs;
using ApiCartobani.Domain.Attributs.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateAttributTests
{
    private readonly Faker _faker;

    public CreateAttributTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_attribut()
    {
        // Arrange
        var attributToCreate = new FakeAttributForCreationDto().Generate();
        
        // Act
        var fakeAttribut = Attribut.Create(attributToCreate);

        // Assert
        fakeAttribut.Nom.Should().Be(attributToCreate.Nom);
        fakeAttribut.Requis.Should().Be(attributToCreate.Requis);
        fakeAttribut.TypeDonnee.Should().Be(attributToCreate.TypeDonnee);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeAttribut = FakeAttribut.Generate();

        // Assert
        fakeAttribut.DomainEvents.Count.Should().Be(1);
        fakeAttribut.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(AttributCreated));
    }
}