namespace ApiCartobani.FunctionalTests.FunctionalTests.PiecesJointes;

using ApiCartobani.SharedTestHelpers.Fakes.PieceJointe;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetPieceJointeListTests : TestBase
{
    [Test]
    public async Task get_piecejointe_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.PiecesJointes.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}