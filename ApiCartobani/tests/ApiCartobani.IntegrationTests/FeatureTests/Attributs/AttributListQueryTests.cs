namespace ApiCartobani.IntegrationTests.FeatureTests.Attributs;

using ApiCartobani.Domain.Attributs.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Attributs.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class AttributListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_attribut_list()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        var fakeAttributTwo = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        var queryParameters = new AttributParametersDto();

        await InsertAsync(fakeAttributOne, fakeAttributTwo);

        // Act
        var query = new GetAttributList.Query(queryParameters);
        var attributs = await SendAsync(query);

        // Assert
        attributs.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}