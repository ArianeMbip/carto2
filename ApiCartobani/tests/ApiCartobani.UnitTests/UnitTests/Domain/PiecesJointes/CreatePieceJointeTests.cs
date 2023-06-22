namespace ApiCartobani.UnitTests.UnitTests.Domain.PiecesJointes;

using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.PiecesJointes.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreatePieceJointeTests
{
    private readonly Faker _faker;

    public CreatePieceJointeTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_pieceJointe()
    {
        // Arrange
        var pieceJointeToCreate = new FakePieceJointeForCreationDto().Generate();
        
        // Act
        var fakePieceJointe = PieceJointe.Create(pieceJointeToCreate);

        // Assert
        fakePieceJointe.Nom.Should().Be(pieceJointeToCreate.Nom);
        fakePieceJointe.Chemin.Should().Be(pieceJointeToCreate.Chemin);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakePieceJointe = FakePieceJointe.Generate();

        // Assert
        fakePieceJointe.DomainEvents.Count.Should().Be(1);
        fakePieceJointe.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(PieceJointeCreated));
    }
}