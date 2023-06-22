namespace ApiCartobani.UnitTests.UnitTests.Domain.Univers;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using ApiCartobani.Domain.Univers;
using ApiCartobani.Domain.Univers.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateUniversTests
{
    private readonly Faker _faker;

    public CreateUniversTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_univers()
    {
        // Arrange
        var universToCreate = new FakeUniversForCreationDto().Generate();
        
        // Act
        var fakeUnivers = Univers.Create(universToCreate);

        // Assert
        fakeUnivers.Nom.Should().Be(universToCreate.Nom);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeUnivers = FakeUnivers.Generate();

        // Assert
        fakeUnivers.DomainEvents.Count.Should().Be(1);
        fakeUnivers.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UniversCreated));
    }
}