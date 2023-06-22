namespace ApiCartobani.FunctionalTests.FunctionalTests.Icones;

using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteIconeTests : TestBase
{
    [Test]
    public async Task delete_icone_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeIcone = FakeIcone.Generate(new FakeIconeForCreationDto().Generate());
        await InsertAsync(fakeIcone);

        // Act
        var route = ApiRoutes.Icones.Delete.Replace(ApiRoutes.Icones.Id, fakeIcone.Id.ToString());
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}