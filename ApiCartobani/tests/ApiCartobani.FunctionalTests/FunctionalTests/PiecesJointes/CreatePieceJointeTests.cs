namespace ApiCartobani.FunctionalTests.FunctionalTests.PiecesJointes;

using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreatePieceJointeTests : TestBase
{
    [Test]
    public async Task create_piecejointe_returns_created_using_valid_dto()
    {
        // Arrange
        var fakePieceJointe = new FakePieceJointeForCreationDto().Generate();

        // Act
        var route = ApiRoutes.PiecesJointes.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakePieceJointe);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}