namespace ApiCartobani.IntegrationTests.FeatureTests.InterfacesUtilisateur;

using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.InterfacesUtilisateur.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddInterfaceUtilisateurCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_interfaceutilisateur_to_db()
    {
        // Arrange
        var fakeInterfaceUtilisateurOne = new FakeInterfaceUtilisateurForCreationDto().Generate();

        // Act
        var command = new AddInterfaceUtilisateur.Command(fakeInterfaceUtilisateurOne);
        var interfaceUtilisateurReturned = await SendAsync(command);
        var interfaceUtilisateurCreated = await ExecuteDbContextAsync(db => db.InterfacesUtilisateur
            .FirstOrDefaultAsync(i => i.Id == interfaceUtilisateurReturned.Id));

        // Assert
        interfaceUtilisateurReturned.Nom.Should().Be(fakeInterfaceUtilisateurOne.Nom);
        interfaceUtilisateurReturned.Image.Should().Be(fakeInterfaceUtilisateurOne.Image);

        interfaceUtilisateurCreated.Nom.Should().Be(fakeInterfaceUtilisateurOne.Nom);
        interfaceUtilisateurCreated.Image.Should().Be(fakeInterfaceUtilisateurOne.Image);
    }
}