namespace ApiCartobani.IntegrationTests.FeatureTests.RolePrivileges;

using ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;
using ApiCartobani.Domain.RolePrivileges.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.RolePrivileges.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateRolePrivilegeCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_roleprivilege_in_db()
    {
        // Arrange
        var fakeRolePrivilegeOne = FakeRolePrivilege.Generate(new FakeRolePrivilegeForCreationDto().Generate());
        var updatedRolePrivilegeDto = new FakeRolePrivilegeForUpdateDto().Generate();
        await InsertAsync(fakeRolePrivilegeOne);

        var rolePrivilege = await ExecuteDbContextAsync(db => db.RolePrivileges
            .FirstOrDefaultAsync(r => r.Id == fakeRolePrivilegeOne.Id));
        var id = rolePrivilege.Id;

        // Act
        var command = new UpdateRolePrivilege.Command(id, updatedRolePrivilegeDto);
        await SendAsync(command);
        var updatedRolePrivilege = await ExecuteDbContextAsync(db => db.RolePrivileges.FirstOrDefaultAsync(r => r.Id == id));

        // Assert
        updatedRolePrivilege.Nom.Should().Be(updatedRolePrivilegeDto.Nom);
        updatedRolePrivilege.Lire.Should().Be(updatedRolePrivilegeDto.Lire);
        updatedRolePrivilege.Ecrire.Should().Be(updatedRolePrivilegeDto.Ecrire);
        updatedRolePrivilege.Modifier.Should().Be(updatedRolePrivilegeDto.Modifier);
        updatedRolePrivilege.Supprimer.Should().Be(updatedRolePrivilegeDto.Supprimer);
        updatedRolePrivilege.Valider.Should().Be(updatedRolePrivilegeDto.Valider);
        updatedRolePrivilege.Archiver.Should().Be(updatedRolePrivilegeDto.Archiver);
        updatedRolePrivilege.Generer.Should().Be(updatedRolePrivilegeDto.Generer);
    }
}