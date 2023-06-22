namespace ApiCartobani.IntegrationTests.FeatureTests.RolePrivileges;

using ApiCartobani.Domain.RolePrivileges.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.RolePrivileges.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class RolePrivilegeListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_roleprivilege_list()
    {
        // Arrange
        var fakeRolePrivilegeOne = FakeRolePrivilege.Generate(new FakeRolePrivilegeForCreationDto().Generate());
        var fakeRolePrivilegeTwo = FakeRolePrivilege.Generate(new FakeRolePrivilegeForCreationDto().Generate());
        var queryParameters = new RolePrivilegeParametersDto();

        await InsertAsync(fakeRolePrivilegeOne, fakeRolePrivilegeTwo);

        // Act
        var query = new GetRolePrivilegeList.Query(queryParameters);
        var rolePrivileges = await SendAsync(query);

        // Assert
        rolePrivileges.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}