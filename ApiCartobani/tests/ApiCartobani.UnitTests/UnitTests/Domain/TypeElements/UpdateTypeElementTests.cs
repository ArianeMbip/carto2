namespace ApiCartobani.UnitTests.UnitTests.Domain.TypeElements;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.TypeElements.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateTypeElementTests
{
    private readonly Faker _faker;

    public UpdateTypeElementTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_typeElement()
    {
        // Arrange
        var fakeTypeElement = FakeTypeElement.Generate();
        var updatedTypeElement = new FakeTypeElementForUpdateDto().Generate();
        
        // Act
        fakeTypeElement.Update(updatedTypeElement);

        // Assert
        fakeTypeElement.Nom.Should().Be(updatedTypeElement.Nom);
        fakeTypeElement.Type.Should().Be(updatedTypeElement.Type);
        fakeTypeElement.Icone.Should().Be(updatedTypeElement.Icone);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeTypeElement = FakeTypeElement.Generate();
        var updatedTypeElement = new FakeTypeElementForUpdateDto().Generate();
        fakeTypeElement.DomainEvents.Clear();
        
        // Act
        fakeTypeElement.Update(updatedTypeElement);

        // Assert
        fakeTypeElement.DomainEvents.Count.Should().Be(1);
        fakeTypeElement.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(TypeElementUpdated));
    }
}