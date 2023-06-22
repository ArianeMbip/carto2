namespace ApiCartobani.UnitTests.UnitTests.Domain.PiecesJointes;

using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.PiecesJointes.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdatePieceJointeTests
{
    private readonly Faker _faker;

    public UpdatePieceJointeTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_pieceJointe()
    {
        // Arrange
        var fakePieceJointe = FakePieceJointe.Generate();
        var updatedPieceJointe = new FakePieceJointeForUpdateDto().Generate();
        
        // Act
        fakePieceJointe.Update(updatedPieceJointe);

        // Assert
        fakePieceJointe.Nom.Should().Be(updatedPieceJointe.Nom);
        fakePieceJointe.Chemin.Should().Be(updatedPieceJointe.Chemin);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakePieceJointe = FakePieceJointe.Generate();
        var updatedPieceJointe = new FakePieceJointeForUpdateDto().Generate();
        fakePieceJointe.DomainEvents.Clear();
        
        // Act
        fakePieceJointe.Update(updatedPieceJointe);

        // Assert
        fakePieceJointe.DomainEvents.Count.Should().Be(1);
        fakePieceJointe.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(PieceJointeUpdated));
    }
}