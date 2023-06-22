namespace ApiCartobani.IntegrationTests.FeatureTests.PiecesJointes;

using ApiCartobani.Domain.PiecesJointes.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.PiecesJointes.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class PieceJointeListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_piecejointe_list()
    {
        // Arrange
        var fakePieceJointeOne = FakePieceJointe.Generate(new FakePieceJointeForCreationDto().Generate());
        var fakePieceJointeTwo = FakePieceJointe.Generate(new FakePieceJointeForCreationDto().Generate());
        var queryParameters = new PieceJointeParametersDto();

        await InsertAsync(fakePieceJointeOne, fakePieceJointeTwo);

        // Act
        var query = new GetPieceJointeList.Query(queryParameters);
        var piecesJointes = await SendAsync(query);

        // Assert
        piecesJointes.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}