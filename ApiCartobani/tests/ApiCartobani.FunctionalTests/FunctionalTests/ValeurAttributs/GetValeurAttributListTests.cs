namespace ApiCartobani.FunctionalTests.FunctionalTests.ValeurAttributs;

using ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetValeurAttributListTests : TestBase
{
    [Test]
    public async Task get_valeurattribut_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.ValeurAttributs.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}