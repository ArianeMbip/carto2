namespace ApiCartobani.FunctionalTests.FunctionalTests.Fonctionnalites;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetFonctionnaliteListTests : TestBase
{
    [Test]
    public async Task get_fonctionnalite_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Fonctionnalites.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}