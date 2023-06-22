namespace ApiCartobani.FunctionalTests.FunctionalTests.PiecesJointes;

using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdatePieceJointeRecordTests : TestBase
{
    [Test]
    public async Task put_piecejointe_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakePieceJointe = FakePieceJointe.Generate(new FakePieceJointeForCreationDto().Generate());
        var updatedPieceJointeDto = new FakePieceJointeForUpdateDto().Generate();
        await InsertAsync(fakePieceJointe);

        // Act
        var route = ApiRoutes.PiecesJointes.Put.Replace(ApiRoutes.PiecesJointes.Id, fakePieceJointe.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedPieceJointeDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}