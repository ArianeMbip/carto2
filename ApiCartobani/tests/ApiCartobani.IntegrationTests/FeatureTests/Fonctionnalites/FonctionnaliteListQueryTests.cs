namespace ApiCartobani.IntegrationTests.FeatureTests.Fonctionnalites;

using ApiCartobani.Domain.Fonctionnalites.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Fonctionnalites.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class FonctionnaliteListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_fonctionnalite_list()
    {
        // Arrange
        var fakeFonctionnaliteOne = FakeFonctionnalite.Generate(new FakeFonctionnaliteForCreationDto().Generate());
        var fakeFonctionnaliteTwo = FakeFonctionnalite.Generate(new FakeFonctionnaliteForCreationDto().Generate());
        var queryParameters = new FonctionnaliteParametersDto();

        await InsertAsync(fakeFonctionnaliteOne, fakeFonctionnaliteTwo);

        // Act
        var query = new GetFonctionnaliteList.Query(queryParameters);
        var fonctionnalites = await SendAsync(query);

        // Assert
        fonctionnalites.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}