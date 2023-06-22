namespace ApiCartobani.IntegrationTests.FeatureTests.RolePrivileges;

using ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;
using ApiCartobani.Domain.RolePrivileges.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class RolePrivilegeQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_roleprivilege_with_accurate_props()
    {
        // Arrange
        var fakeRolePrivilegeOne = FakeRolePrivilege.Generate(new FakeRolePrivilegeForCreationDto().Generate());
        await InsertAsync(fakeRolePrivilegeOne);

        // Act
        var query = new GetRolePrivilege.Query(fakeRolePrivilegeOne.Id);
        var rolePrivilege = await SendAsync(query);

        // Assert
        rolePrivilege.Nom.Should().Be(fakeRolePrivilegeOne.Nom);
        rolePrivilege.Lire.Should().Be(fakeRolePrivilegeOne.Lire);
        rolePrivilege.Ecrire.Should().Be(fakeRolePrivilegeOne.Ecrire);
        rolePrivilege.Modifier.Should().Be(fakeRolePrivilegeOne.Modifier);
        rolePrivilege.Supprimer.Should().Be(fakeRolePrivilegeOne.Supprimer);
        rolePrivilege.Valider.Should().Be(fakeRolePrivilegeOne.Valider);
        rolePrivilege.Archiver.Should().Be(fakeRolePrivilegeOne.Archiver);
        rolePrivilege.Generer.Should().Be(fakeRolePrivilegeOne.Generer);
    }

    [Test]
    public async Task get_roleprivilege_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetRolePrivilege.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}