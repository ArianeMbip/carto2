namespace ApiCartobani.IntegrationTests.FeatureTests.PiecesJointes;

using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using ApiCartobani.Domain.PiecesJointes.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeletePieceJointeCommandTests : TestBase
{
    [Test]
    public async Task can_delete_piecejointe_from_db()
    {
        // Arrange
        var fakePieceJointeOne = FakePieceJointe.Generate(new FakePieceJointeForCreationDto().Generate());
        await InsertAsync(fakePieceJointeOne);
        var pieceJointe = await ExecuteDbContextAsync(db => db.PiecesJointes
            .FirstOrDefaultAsync(p => p.Id == fakePieceJointeOne.Id));

        // Act
        var command = new DeletePieceJointe.Command(pieceJointe.Id);
        await SendAsync(command);
        var pieceJointeResponse = await ExecuteDbContextAsync(db => db.PiecesJointes.CountAsync(p => p.Id == pieceJointe.Id));

        // Assert
        pieceJointeResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_piecejointe_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeletePieceJointe.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_piecejointe_from_db()
    {
        // Arrange
        var fakePieceJointeOne = FakePieceJointe.Generate(new FakePieceJointeForCreationDto().Generate());
        await InsertAsync(fakePieceJointeOne);
        var pieceJointe = await ExecuteDbContextAsync(db => db.PiecesJointes
            .FirstOrDefaultAsync(p => p.Id == fakePieceJointeOne.Id));

        // Act
        var command = new DeletePieceJointe.Command(pieceJointe.Id);
        await SendAsync(command);
        var deletedPieceJointe = await ExecuteDbContextAsync(db => db.PiecesJointes
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == pieceJointe.Id));

        // Assert
        deletedPieceJointe?.IsDeleted.Should().BeTrue();
    }
}