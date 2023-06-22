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

public class DeleteActifTests : TestBase
{
    [Test]
    public async Task delete_actif_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

         fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

        var fakeActif = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.TypeActif, _ => fakeTypeElementOne.Id)
            .RuleFor(a => a.PreVersion, _ => fakeActifParentOne.Id)
            .RuleFor(a => a.Hierarchie, _ => fakeActifParentOne.Id).Generate());
        await InsertAsync(fakeActif);

        // Act
        var route = ApiRoutes.Actifs.Delete.Replace(ApiRoutes.Actifs.Id, fakeActif.Id.ToString());
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}