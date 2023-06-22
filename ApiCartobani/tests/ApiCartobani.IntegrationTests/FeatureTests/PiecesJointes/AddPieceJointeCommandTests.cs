namespace ApiCartobani.IntegrationTests.FeatureTests.PiecesJointes;

using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.PiecesJointes.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddPieceJointeCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_piecejointe_to_db()
    {
        // Arrange
        var fakePieceJointeOne = new FakePieceJointeForCreationDto().Generate();

        // Act
        var command = new AddPieceJointe.Command(fakePieceJointeOne);
        var pieceJointeReturned = await SendAsync(command);
        var pieceJointeCreated = await ExecuteDbContextAsync(db => db.PiecesJointes
            .FirstOrDefaultAsync(p => p.Id == pieceJointeReturned.Id));

        // Assert
        pieceJointeReturned.Nom.Should().Be(fakePieceJointeOne.Nom);
        pieceJointeReturned.Chemin.Should().Be(fakePieceJointeOne.Chemin);

        pieceJointeCreated.Nom.Should().Be(fakePieceJointeOne.Nom);
        pieceJointeCreated.Chemin.Should().Be(fakePieceJointeOne.Chemin);
    }
}