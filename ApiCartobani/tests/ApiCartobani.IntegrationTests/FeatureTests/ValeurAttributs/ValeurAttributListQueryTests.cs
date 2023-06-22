namespace ApiCartobani.IntegrationTests.FeatureTests.ValeurAttributs;

using ApiCartobani.Domain.ValeurAttributs.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.ValeurAttributs.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.SharedTestHelpers.Fakes.Environnement;

public class ValeurAttributListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_valeurattribut_list()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        var fakeAttributTwo = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttributOne, fakeAttributTwo);

        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        var fakeEnvironnementTwo = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnementOne, fakeEnvironnementTwo);

        var fakeValeurAttributOne = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Attribut, _ => fakeAttributOne.Id)
            .RuleFor(v => v.Environnement, _ => fakeEnvironnementOne.Id).Generate());
        var fakeValeurAttributTwo = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Attribut, _ => fakeAttributTwo.Id)
            .RuleFor(v => v.Environnement, _ => fakeEnvironnementTwo.Id).Generate());
        var queryParameters = new ValeurAttributParametersDto();

        await InsertAsync(fakeValeurAttributOne, fakeValeurAttributTwo);

        // Act
        var query = new GetValeurAttributList.Query(queryParameters);
        var valeurAttributs = await SendAsync(query);

        // Assert
        valeurAttributs.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}