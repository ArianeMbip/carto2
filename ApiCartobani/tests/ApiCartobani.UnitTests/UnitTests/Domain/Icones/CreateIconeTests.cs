namespace ApiCartobani.UnitTests.UnitTests.Domain.Icones;

using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using ApiCartobani.Domain.Icones;
using ApiCartobani.Domain.Icones.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateIconeTests
{
    private readonly Faker _faker;

    public CreateIconeTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_icone()
    {
        // Arrange
        var iconeToCreate = new FakeIconeForCreationDto().Generate();
        
        // Act
        var fakeIcone = Icone.Create(iconeToCreate);

        // Assert
        fakeIcone.Url.Should().Be(iconeToCreate.Url);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeIcone = FakeIcone.Generate();

        // Assert
        fakeIcone.DomainEvents.Count.Should().Be(1);
        fakeIcone.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(IconeCreated));
    }
}