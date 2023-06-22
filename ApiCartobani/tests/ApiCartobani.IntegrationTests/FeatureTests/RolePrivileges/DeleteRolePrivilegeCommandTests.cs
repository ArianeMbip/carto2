namespace ApiCartobani.IntegrationTests.FeatureTests.RolePrivileges;

using ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;
using ApiCartobani.Domain.RolePrivileges.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteRolePrivilegeCommandTests : TestBase
{
    [Test]
    public async Task can_delete_roleprivilege_from_db()
    {
        // Arrange
        var fakeRolePrivilegeOne = FakeRolePrivilege.Generate(new FakeRolePrivilegeForCreationDto().Generate());
        await InsertAsync(fakeRolePrivilegeOne);
        var rolePrivilege = await ExecuteDbContextAsync(db => db.RolePrivileges
            .FirstOrDefaultAsync(r => r.Id == fakeRolePrivilegeOne.Id));

        // Act
        var command = new DeleteRolePrivilege.Command(rolePrivilege.Id);
        await SendAsync(command);
        var rolePrivilegeResponse = await ExecuteDbContextAsync(db => db.RolePrivileges.CountAsync(r => r.Id == rolePrivilege.Id));

        // Assert
        rolePrivilegeResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_roleprivilege_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteRolePrivilege.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_roleprivilege_from_db()
    {
        // Arrange
        var fakeRolePrivilegeOne = FakeRolePrivilege.Generate(new FakeRolePrivilegeForCreationDto().Generate());
        await InsertAsync(fakeRolePrivilegeOne);
        var rolePrivilege = await ExecuteDbContextAsync(db => db.RolePrivileges
            .FirstOrDefaultAsync(r => r.Id == fakeRolePrivilegeOne.Id));

        // Act
        var command = new DeleteRolePrivilege.Command(rolePrivilege.Id);
        await SendAsync(command);
        var deletedRolePrivilege = await ExecuteDbContextAsync(db => db.RolePrivileges
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == rolePrivilege.Id));

        // Assert
        deletedRolePrivilege?.IsDeleted.Should().BeTrue();
    }
}