namespace ApiCartobani.FunctionalTests.FunctionalTests.InterfacesUtilisateur;

using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetInterfaceUtilisateurTests : TestBase
{
    [Test]
    public async Task get_interfaceutilisateur_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeInterfaceUtilisateur = FakeInterfaceUtilisateur.Generate(new FakeInterfaceUtilisateurForCreationDto().Generate());
        await InsertAsync(fakeInterfaceUtilisateur);

        // Act
        var route = ApiRoutes.InterfacesUtilisateur.GetRecord.Replace(ApiRoutes.InterfacesUtilisateur.Id, fakeInterfaceUtilisateur.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}