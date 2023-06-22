namespace ApiCartobani.IntegrationTests.FeatureTests.Univers;

using ApiCartobani.Domain.Univers.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Univers.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UniversListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_univers_list()
    {
        // Arrange
        var fakeUniversOne = FakeUnivers.Generate(new FakeUniversForCreationDto().Generate());
        var fakeUniversTwo = FakeUnivers.Generate(new FakeUniversForCreationDto().Generate());
        var queryParameters = new UniversParametersDto();

        await InsertAsync(fakeUniversOne, fakeUniversTwo);

        // Act
        var query = new GetUniversList.Query(queryParameters);
        var univers = await SendAsync(query);

        // Assert
        univers.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}