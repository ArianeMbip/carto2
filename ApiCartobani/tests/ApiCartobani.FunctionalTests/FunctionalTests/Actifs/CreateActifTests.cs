namespace ApiCartobani.FunctionalTests.FunctionalTests.Actifs;

using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.FunctionalTests.TestUtilities;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateActifTests : TestBase
{
    [Test]
    public async Task create_actif_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

         fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeActif = new FakeActifForCreationDto()
            .RuleFor(a => a.TypeActif, _ => fakeTypeElementOne.Id)
            
            .RuleFor(a => a.PreVersion, _ => fakeActifOne.Id)
            
            .RuleFor(a => a.Hierarchie, _ => fakeActifOne.Id)
            .Generate();

        // Act
        var route = ApiRoutes.Actifs.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeActif);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}