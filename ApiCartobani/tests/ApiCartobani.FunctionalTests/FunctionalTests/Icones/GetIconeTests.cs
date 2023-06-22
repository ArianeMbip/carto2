namespace ApiCartobani.FunctionalTests.FunctionalTests.Icones;

using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetIconeTests : TestBase
{
    [Test]
    public async Task get_icone_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeIcone = FakeIcone.Generate(new FakeIconeForCreationDto().Generate());
        await InsertAsync(fakeIcone);

        // Act
        var route = ApiRoutes.Icones.GetRecord.Replace(ApiRoutes.Icones.Id, fakeIcone.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}