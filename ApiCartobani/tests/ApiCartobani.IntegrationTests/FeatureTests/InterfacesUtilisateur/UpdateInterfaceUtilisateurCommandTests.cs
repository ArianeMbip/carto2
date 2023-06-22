namespace ApiCartobani.IntegrationTests.FeatureTests.InterfacesUtilisateur;

using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.InterfacesUtilisateur.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateInterfaceUtilisateurCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_interfaceutilisateur_in_db()
    {
        // Arrange
        var fakeInterfaceUtilisateurOne = FakeInterfaceUtilisateur.Generate(new FakeInterfaceUtilisateurForCreationDto().Generate());
        var updatedInterfaceUtilisateurDto = new FakeInterfaceUtilisateurForUpdateDto().Generate();
        await InsertAsync(fakeInterfaceUtilisateurOne);

        var interfaceUtilisateur = await ExecuteDbContextAsync(db => db.InterfacesUtilisateur
            .FirstOrDefaultAsync(i => i.Id == fakeInterfaceUtilisateurOne.Id));
        var id = interfaceUtilisateur.Id;

        // Act
        var command = new UpdateInterfaceUtilisateur.Command(id, updatedInterfaceUtilisateurDto);
        await SendAsync(command);
        var updatedInterfaceUtilisateur = await ExecuteDbContextAsync(db => db.InterfacesUtilisateur.FirstOrDefaultAsync(i => i.Id == id));

        // Assert
        updatedInterfaceUtilisateur.Nom.Should().Be(updatedInterfaceUtilisateurDto.Nom);
        updatedInterfaceUtilisateur.Image.Should().Be(updatedInterfaceUtilisateurDto.Image);
    }
}