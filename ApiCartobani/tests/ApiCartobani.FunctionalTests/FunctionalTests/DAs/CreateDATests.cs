namespace ApiCartobani.FunctionalTests.FunctionalTests.DAs;

using ApiCartobani.SharedTestHelpers.Fakes.DA;
using ApiCartobani.FunctionalTests.TestUtilities;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateDATests : TestBase
{
    [Test]
    public async Task create_da_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeDA = new FakeDAForCreationDto()
            .RuleFor(d => d.IdActif, _ => fakeActifOne.Id)
            .Generate();

        // Act
        var route = ApiRoutes.DAs.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeDA);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}