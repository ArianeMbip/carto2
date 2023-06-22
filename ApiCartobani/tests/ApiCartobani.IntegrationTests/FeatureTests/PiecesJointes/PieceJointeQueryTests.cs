namespace ApiCartobani.IntegrationTests.FeatureTests.PiecesJointes;

using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using ApiCartobani.Domain.PiecesJointes.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class PieceJointeQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_piecejointe_with_accurate_props()
    {
        // Arrange
        var fakePieceJointeOne = FakePieceJointe.Generate(new FakePieceJointeForCreationDto().Generate());
        await InsertAsync(fakePieceJointeOne);

        // Act
        var query = new GetPieceJointe.Query(fakePieceJointeOne.Id);
        var pieceJointe = await SendAsync(query);

        // Assert
        pieceJointe.Nom.Should().Be(fakePieceJointeOne.Nom);
        pieceJointe.Chemin.Should().Be(fakePieceJointeOne.Chemin);
    }

    [Test]
    public async Task get_piecejointe_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetPieceJointe.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}