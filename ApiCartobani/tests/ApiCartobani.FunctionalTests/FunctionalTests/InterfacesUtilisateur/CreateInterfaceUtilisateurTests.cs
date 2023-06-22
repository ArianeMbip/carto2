namespace ApiCartobani.FunctionalTests.FunctionalTests.InterfacesUtilisateur;

using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateInterfaceUtilisateurTests : TestBase
{
    [Test]
    public async Task create_interfaceutilisateur_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeInterfaceUtilisateur = new FakeInterfaceUtilisateurForCreationDto().Generate();

        // Act
        var route = ApiRoutes.InterfacesUtilisateur.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeInterfaceUtilisateur);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}