namespace ApiCartobani.FunctionalTests.FunctionalTests.Composants;

using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetComposantListTests : TestBase
{
    [Test]
    public async Task get_composant_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Composants.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}