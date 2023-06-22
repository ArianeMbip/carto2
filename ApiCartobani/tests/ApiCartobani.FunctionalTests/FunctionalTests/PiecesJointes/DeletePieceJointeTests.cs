namespace ApiCartobani.FunctionalTests.FunctionalTests.PiecesJointes;

using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeletePieceJointeTests : TestBase
{
    [Test]
    public async Task delete_piecejointe_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakePieceJointe = FakePieceJointe.Generate(new FakePieceJointeForCreationDto().Generate());
        await InsertAsync(fakePieceJointe);

        // Act
        var route = ApiRoutes.PiecesJointes.Delete.Replace(ApiRoutes.PiecesJointes.Id, fakePieceJointe.Id.ToString());
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}