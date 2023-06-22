namespace ApiCartobani.IntegrationTests.FeatureTests.DAs;

using ApiCartobani.Domain.DAs.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.DA;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.DAs.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;

public class DAListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_da_list()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        var fakeActifTwo = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne, fakeActifTwo);

        var fakeDAOne = FakeDA.Generate(new FakeDAForCreationDto()
            .RuleFor(d => d.IdActif, _ => fakeActifOne.Id).Generate());
        var fakeDATwo = FakeDA.Generate(new FakeDAForCreationDto()
            .RuleFor(d => d.IdActif, _ => fakeActifTwo.Id).Generate());
        var queryParameters = new DAParametersDto();

        await InsertAsync(fakeDAOne, fakeDATwo);

        // Act
        var query = new GetDAList.Query(queryParameters);
        var dAs = await SendAsync(query);

        // Assert
        dAs.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}