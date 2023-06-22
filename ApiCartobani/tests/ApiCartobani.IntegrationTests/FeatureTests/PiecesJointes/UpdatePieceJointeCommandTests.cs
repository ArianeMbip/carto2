namespace ApiCartobani.IntegrationTests.FeatureTests.PiecesJointes;

using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using ApiCartobani.Domain.PiecesJointes.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.PiecesJointes.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdatePieceJointeCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_piecejointe_in_db()
    {
        // Arrange
        var fakePieceJointeOne = FakePieceJointe.Generate(new FakePieceJointeForCreationDto().Generate());
        var updatedPieceJointeDto = new FakePieceJointeForUpdateDto().Generate();
        await InsertAsync(fakePieceJointeOne);

        var pieceJointe = await ExecuteDbContextAsync(db => db.PiecesJointes
            .FirstOrDefaultAsync(p => p.Id == fakePieceJointeOne.Id));
        var id = pieceJointe.Id;

        // Act
        var command = new UpdatePieceJointe.Command(id, updatedPieceJointeDto);
        await SendAsync(command);
        var updatedPieceJointe = await ExecuteDbContextAsync(db => db.PiecesJointes.FirstOrDefaultAsync(p => p.Id == id));

        // Assert
        updatedPieceJointe.Nom.Should().Be(updatedPieceJointeDto.Nom);
        updatedPieceJointe.Chemin.Should().Be(updatedPieceJointeDto.Chemin);
    }
}