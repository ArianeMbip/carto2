namespace ApiCartobani.FunctionalTests.FunctionalTests.RolePrivileges;

using ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetRolePrivilegeListTests : TestBase
{
    [Test]
    public async Task get_roleprivilege_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.RolePrivileges.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}