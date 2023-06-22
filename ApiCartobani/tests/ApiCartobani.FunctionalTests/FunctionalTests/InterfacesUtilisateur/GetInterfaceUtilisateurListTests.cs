namespace ApiCartobani.FunctionalTests.FunctionalTests.InterfacesUtilisateur;

using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetInterfaceUtilisateurListTests : TestBase
{
    [Test]
    public async Task get_interfaceutilisateur_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.InterfacesUtilisateur.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}