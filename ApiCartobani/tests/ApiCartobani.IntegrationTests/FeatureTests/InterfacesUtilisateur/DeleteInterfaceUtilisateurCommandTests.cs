namespace ApiCartobani.IntegrationTests.FeatureTests.InterfacesUtilisateur;

using ApiCartobani.SharedTestHelpers.Fakes.InterfaceUtilisateur;
using ApiCartobani.Domain.InterfacesUtilisateur.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteInterfaceUtilisateurCommandTests : TestBase
{
    [Test]
    public async Task can_delete_interfaceutilisateur_from_db()
    {
        // Arrange
        var fakeInterfaceUtilisateurOne = FakeInterfaceUtilisateur.Generate(new FakeInterfaceUtilisateurForCreationDto().Generate());
        await InsertAsync(fakeInterfaceUtilisateurOne);
        var interfaceUtilisateur = await ExecuteDbContextAsync(db => db.InterfacesUtilisateur
            .FirstOrDefaultAsync(i => i.Id == fakeInterfaceUtilisateurOne.Id));

        // Act
        var command = new DeleteInterfaceUtilisateur.Command(interfaceUtilisateur.Id);
        await SendAsync(command);
        var interfaceUtilisateurResponse = await ExecuteDbContextAsync(db => db.InterfacesUtilisateur.CountAsync(i => i.Id == interfaceUtilisateur.Id));

        // Assert
        interfaceUtilisateurResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_interfaceutilisateur_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteInterfaceUtilisateur.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_interfaceutilisateur_from_db()
    {
        // Arrange
        var fakeInterfaceUtilisateurOne = FakeInterfaceUtilisateur.Generate(new FakeInterfaceUtilisateurForCreationDto().Generate());
        await InsertAsync(fakeInterfaceUtilisateurOne);
        var interfaceUtilisateur = await ExecuteDbContextAsync(db => db.InterfacesUtilisateur
            .FirstOrDefaultAsync(i => i.Id == fakeInterfaceUtilisateurOne.Id));

        // Act
        var command = new DeleteInterfaceUtilisateur.Command(interfaceUtilisateur.Id);
        await SendAsync(command);
        var deletedInterfaceUtilisateur = await ExecuteDbContextAsync(db => db.InterfacesUtilisateur
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == interfaceUtilisateur.Id));

        // Assert
        deletedInterfaceUtilisateur?.IsDeleted.Should().BeTrue();
    }
}