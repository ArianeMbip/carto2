namespace ApiCartobani.FunctionalTests.FunctionalTests.RolePrivileges;

using ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateRolePrivilegeTests : TestBase
{
    [Test]
    public async Task create_roleprivilege_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeRolePrivilege = new FakeRolePrivilegeForCreationDto().Generate();

        // Act
        var route = ApiRoutes.RolePrivileges.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeRolePrivilege);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}