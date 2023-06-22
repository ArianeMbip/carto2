namespace ApiCartobani.UnitTests.UnitTests.Domain.TypeElements;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.TypeElements.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateTypeElementTests
{
    private readonly Faker _faker;

    public CreateTypeElementTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_typeElement()
    {
        // Arrange
        var typeElementToCreate = new FakeTypeElementForCreationDto().Generate();
        
        // Act
        var fakeTypeElement = TypeElement.Create(typeElementToCreate);

        // Assert
        fakeTypeElement.Nom.Should().Be(typeElementToCreate.Nom);
        fakeTypeElement.Type.Should().Be(typeElementToCreate.Type);
        fakeTypeElement.Icone.Should().Be(typeElementToCreate.Icone);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeTypeElement = FakeTypeElement.Generate();

        // Assert
        fakeTypeElement.DomainEvents.Count.Should().Be(1);
        fakeTypeElement.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(TypeElementCreated));
    }
}