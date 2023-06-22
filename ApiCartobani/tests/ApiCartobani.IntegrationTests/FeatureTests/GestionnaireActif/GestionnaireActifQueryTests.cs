namespace ApiCartobani.IntegrationTests.FeatureTests.GestionnaireActif;

using ApiCartobani.SharedTestHelpers.Fakes.GestionnaireActif;
using ApiCartobani.Domain.GestionnaireActif.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class GestionnaireActifQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_gestionnaireactif_with_accurate_props()
    {
        // Arrange
        var fakeGestionnaireActifOne = FakeGestionnaireActif.Generate(new FakeGestionnaireActifForCreationDto().Generate());
        await InsertAsync(fakeGestionnaireActifOne);

        // Act
        var query = new GetGestionnaireActif.Query(fakeGestionnaireActifOne.Id);
        var gestionnaireActif = await SendAsync(query);

        // Assert
        gestionnaireActif.Nom.Should().Be(fakeGestionnaireActifOne.Nom);
        gestionnaireActif.CUID.Should().Be(fakeGestionnaireActifOne.CUID);
    }

    [Test]
    public async Task get_gestionnaireactif_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetGestionnaireActif.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}