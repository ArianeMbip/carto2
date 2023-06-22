namespace ApiCartobani.FunctionalTests.FunctionalTests.Environnements;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetEnvironnementListTests : TestBase
{
    [Test]
    public async Task get_environnement_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Environnements.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}