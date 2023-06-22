namespace ApiCartobani.IntegrationTests.FeatureTests.InterfacesUtilisateur;

using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class InterfaceUtilisateurQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_interfaceutilisateur_with_accurate_props()
    {
        // Arrange
        var fakeInterfaceUtilisateurOne = FakeInterfaceUtilisateur.Generate(new FakeInterfaceUtilisateurForCreationDto().Generate());
        await InsertAsync(fakeInterfaceUtilisateurOne);

        // Act
        var query = new GetInterfaceUtilisateur.Query(fakeInterfaceUtilisateurOne.Id);
        var interfaceUtilisateur = await SendAsync(query);

        // Assert
        interfaceUtilisateur.Nom.Should().Be(fakeInterfaceUtilisateurOne.Nom);
        interfaceUtilisateur.Image.Should().Be(fakeInterfaceUtilisateurOne.Image);
    }

    [Test]
    public async Task get_interfaceutilisateur_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetInterfaceUtilisateur.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}