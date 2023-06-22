namespace ApiCartobani.IntegrationTests.FeatureTests.RolePrivileges;

using ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.RolePrivileges.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddRolePrivilegeCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_roleprivilege_to_db()
    {
        // Arrange
        var fakeRolePrivilegeOne = new FakeRolePrivilegeForCreationDto().Generate();

        // Act
        var command = new AddRolePrivilege.Command(fakeRolePrivilegeOne);
        var rolePrivilegeReturned = await SendAsync(command);
        var rolePrivilegeCreated = await ExecuteDbContextAsync(db => db.RolePrivileges
            .FirstOrDefaultAsync(r => r.Id == rolePrivilegeReturned.Id));

        // Assert
        rolePrivilegeReturned.Nom.Should().Be(fakeRolePrivilegeOne.Nom);
        rolePrivilegeReturned.Lire.Should().Be(fakeRolePrivilegeOne.Lire);
        rolePrivilegeReturned.Ecrire.Should().Be(fakeRolePrivilegeOne.Ecrire);
        rolePrivilegeReturned.Modifier.Should().Be(fakeRolePrivilegeOne.Modifier);
        rolePrivilegeReturned.Supprimer.Should().Be(fakeRolePrivilegeOne.Supprimer);
        rolePrivilegeReturned.Valider.Should().Be(fakeRolePrivilegeOne.Valider);
        rolePrivilegeReturned.Archiver.Should().Be(fakeRolePrivilegeOne.Archiver);
        rolePrivilegeReturned.Generer.Should().Be(fakeRolePrivilegeOne.Generer);

        rolePrivilegeCreated.Nom.Should().Be(fakeRolePrivilegeOne.Nom);
        rolePrivilegeCreated.Lire.Should().Be(fakeRolePrivilegeOne.Lire);
        rolePrivilegeCreated.Ecrire.Should().Be(fakeRolePrivilegeOne.Ecrire);
        rolePrivilegeCreated.Modifier.Should().Be(fakeRolePrivilegeOne.Modifier);
        rolePrivilegeCreated.Supprimer.Should().Be(fakeRolePrivilegeOne.Supprimer);
        rolePrivilegeCreated.Valider.Should().Be(fakeRolePrivilegeOne.Valider);
        rolePrivilegeCreated.Archiver.Should().Be(fakeRolePrivilegeOne.Archiver);
        rolePrivilegeCreated.Generer.Should().Be(fakeRolePrivilegeOne.Generer);
    }
}