namespace ApiCartobani.FunctionalTests.FunctionalTests.Attributs;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetAttributListTests : TestBase
{
    [Test]
    public async Task get_attribut_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Attributs.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}