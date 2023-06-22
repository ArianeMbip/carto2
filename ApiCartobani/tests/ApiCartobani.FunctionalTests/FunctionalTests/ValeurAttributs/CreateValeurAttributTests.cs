namespace ApiCartobani.FunctionalTests.FunctionalTests.ValeurAttributs;

using ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;
using ApiCartobani.FunctionalTests.TestUtilities;
using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateValeurAttributTests : TestBase
{
    [Test]
    public async Task create_valeurattribut_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttributOne);

        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnementOne);

        var fakeValeurAttribut = new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Attribut, _ => fakeAttributOne.Id)
            
            .RuleFor(v => v.Environnement, _ => fakeEnvironnementOne.Id)
            .Generate();

        // Act
        var route = ApiRoutes.ValeurAttributs.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeValeurAttribut);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}