namespace ApiCartobani.FunctionalTests.FunctionalTests.RolePrivileges;

using ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateRolePrivilegeRecordTests : TestBase
{
    [Test]
    public async Task put_roleprivilege_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeRolePrivilege = FakeRolePrivilege.Generate(new FakeRolePrivilegeForCreationDto().Generate());
        var updatedRolePrivilegeDto = new FakeRolePrivilegeForUpdateDto().Generate();
        await InsertAsync(fakeRolePrivilege);

        // Act
        var route = ApiRoutes.RolePrivileges.Put.Replace(ApiRoutes.RolePrivileges.Id, fakeRolePrivilege.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedRolePrivilegeDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}