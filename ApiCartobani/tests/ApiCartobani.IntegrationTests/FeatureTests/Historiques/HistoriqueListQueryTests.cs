namespace ApiCartobani.IntegrationTests.FeatureTests.Historiques;

using ApiCartobani.Domain.Historiques.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.Historique;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Historiques.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class HistoriqueListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_historique_list()
    {
        // Arrange
        var fakeHistoriqueOne = FakeHistorique.Generate(new FakeHistoriqueForCreationDto().Generate());
        var fakeHistoriqueTwo = FakeHistorique.Generate(new FakeHistoriqueForCreationDto().Generate());
        var queryParameters = new HistoriqueParametersDto();

        await InsertAsync(fakeHistoriqueOne, fakeHistoriqueTwo);

        // Act
        var query = new GetHistoriqueList.Query(queryParameters);
        var historiques = await SendAsync(query);

        // Assert
        historiques.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}