namespace ApiCartobani.FunctionalTests.FunctionalTests.Attributs;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetAttributTests : TestBase
{
    [Test]
    public async Task get_attribut_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeAttribut = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttribut);

        // Act
        var route = ApiRoutes.Attributs.GetRecord.Replace(ApiRoutes.Attributs.Id, fakeAttribut.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}