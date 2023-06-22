namespace ApiCartobani.FunctionalTests.FunctionalTests.Icones;

using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetIconeListTests : TestBase
{
    [Test]
    public async Task get_icone_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Icones.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}