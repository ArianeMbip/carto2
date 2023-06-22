namespace ApiCartobani.IntegrationTests.FeatureTests.Icones;

using ApiCartobani.Domain.Icones.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Icones.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class IconeListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_icone_list()
    {
        // Arrange
        var fakeIconeOne = FakeIcone.Generate(new FakeIconeForCreationDto().Generate());
        var fakeIconeTwo = FakeIcone.Generate(new FakeIconeForCreationDto().Generate());
        var queryParameters = new IconeParametersDto();

        await InsertAsync(fakeIconeOne, fakeIconeTwo);

        // Act
        var query = new GetIconeList.Query(queryParameters);
        var icones = await SendAsync(query);

        // Assert
        icones.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}