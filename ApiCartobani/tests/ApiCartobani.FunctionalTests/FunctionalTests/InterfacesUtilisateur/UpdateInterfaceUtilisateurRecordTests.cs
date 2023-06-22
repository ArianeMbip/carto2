namespace ApiCartobani.FunctionalTests.FunctionalTests.InterfacesUtilisateur;

using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateInterfaceUtilisateurRecordTests : TestBase
{
    [Test]
    public async Task put_interfaceutilisateur_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeInterfaceUtilisateur = FakeInterfaceUtilisateur.Generate(new FakeInterfaceUtilisateurForCreationDto().Generate());
        var updatedInterfaceUtilisateurDto = new FakeInterfaceUtilisateurForUpdateDto().Generate();
        await InsertAsync(fakeInterfaceUtilisateur);

        // Act
        var route = ApiRoutes.InterfacesUtilisateur.Put.Replace(ApiRoutes.InterfacesUtilisateur.Id, fakeInterfaceUtilisateur.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedInterfaceUtilisateurDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}