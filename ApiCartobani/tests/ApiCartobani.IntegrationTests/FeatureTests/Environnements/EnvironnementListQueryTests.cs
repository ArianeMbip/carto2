namespace ApiCartobani.IntegrationTests.FeatureTests.Environnements;

using ApiCartobani.Domain.Environnements.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Environnements.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class EnvironnementListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_environnement_list()
    {
        // Arrange
        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        var fakeEnvironnementTwo = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        var queryParameters = new EnvironnementParametersDto();

        await InsertAsync(fakeEnvironnementOne, fakeEnvironnementTwo);

        // Act
        var query = new GetEnvironnementList.Query(queryParameters);
        var environnements = await SendAsync(query);

        // Assert
        environnements.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}