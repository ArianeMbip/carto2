namespace ApiCartobani.IntegrationTests.FeatureTests.InterfacesUtilisateur;

using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.InterfacesUtilisateur.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class InterfaceUtilisateurListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_interfaceutilisateur_list()
    {
        // Arrange
        var fakeInterfaceUtilisateurOne = FakeInterfaceUtilisateur.Generate(new FakeInterfaceUtilisateurForCreationDto().Generate());
        var fakeInterfaceUtilisateurTwo = FakeInterfaceUtilisateur.Generate(new FakeInterfaceUtilisateurForCreationDto().Generate());
        var queryParameters = new InterfaceUtilisateurParametersDto();

        await InsertAsync(fakeInterfaceUtilisateurOne, fakeInterfaceUtilisateurTwo);

        // Act
        var query = new GetInterfaceUtilisateurList.Query(queryParameters);
        var interfacesUtilisateur = await SendAsync(query);

        // Assert
        interfacesUtilisateur.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}