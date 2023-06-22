namespace ApiCartobani.FunctionalTests.FunctionalTests.DAs;

using ApiCartobani.SharedTestHelpers.Fakes.DA;
using ApiCartobani.FunctionalTests.TestUtilities;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteDATests : TestBase
{
    [Test]
    public async Task delete_da_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeDA = FakeDA.Generate(new FakeDAForCreationDto()
            .RuleFor(d => d.IdActif, _ => fakeActifOne.Id).Generate());
        await InsertAsync(fakeDA);

        // Act
        var route = ApiRoutes.DAs.Delete.Replace(ApiRoutes.DAs.Id, fakeDA.Id.ToString());
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}