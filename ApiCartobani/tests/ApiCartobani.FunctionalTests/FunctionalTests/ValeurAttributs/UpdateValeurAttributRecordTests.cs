namespace ApiCartobani.FunctionalTests.FunctionalTests.ValeurAttributs;

using ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;
using ApiCartobani.FunctionalTests.TestUtilities;
using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateValeurAttributRecordTests : TestBase
{
    [Test]
    public async Task put_valeurattribut_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttributOne);

        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnementOne);

        var fakeValeurAttribut = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Attribut, _ => fakeAttributOne.Id)
            .RuleFor(v => v.Environnement, _ => fakeEnvironnementOne.Id).Generate());
        var updatedValeurAttributDto = new FakeValeurAttributForUpdateDto()
            .RuleFor(v => v.Attribut, _ => fakeAttributOne.Id)
            .RuleFor(v => v.Environnement, _ => fakeEnvironnementOne.Id).Generate();
        await InsertAsync(fakeValeurAttribut);

        // Act
        var route = ApiRoutes.ValeurAttributs.Put.Replace(ApiRoutes.ValeurAttributs.Id, fakeValeurAttribut.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedValeurAttributDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}