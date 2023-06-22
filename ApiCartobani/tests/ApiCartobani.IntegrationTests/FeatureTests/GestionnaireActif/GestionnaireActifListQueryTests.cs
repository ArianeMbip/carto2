namespace ApiCartobani.IntegrationTests.FeatureTests.GestionnaireActif;

using ApiCartobani.Domain.GestionnaireActif.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.GestionnaireActif.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class GestionnaireActifListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_gestionnaireactif_list()
    {
        // Arrange
        var fakeGestionnaireActifOne = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto().Generate());
        var fakeGestionnaireActifTwo = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto().Generate());
        var queryParameters = new GestionnaireActifParametersDto();

        await InsertAsync(fakeGestionnaireActifOne, fakeGestionnaireActifTwo);

        // Act
        var query = new GetGestionnaireActifList.Query(queryParameters);
        var gestionnaireActif = await SendAsync(query);

        // Assert
        gestionnaireActif.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}