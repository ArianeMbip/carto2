namespace ApiCartobani.UnitTests.UnitTests.Domain.RolePrivileges.Features;

using ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;
using ApiCartobani.Domain.RolePrivileges;
using ApiCartobani.Domain.RolePrivileges.Dtos;
using ApiCartobani.Domain.RolePrivileges.Mappings;
using ApiCartobani.Domain.RolePrivileges.Features;
using ApiCartobani.Domain.RolePrivileges.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetRolePrivilegeListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IRolePrivilegeRepository> _rolePrivilegeRepository;

    public GetRolePrivilegeListTests()
    {
        _rolePrivilegeRepository = new Mock<IRolePrivilegeRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_rolePrivilege()
    {
        //Arrange
        var fakeRolePrivilegeOne = FakeRolePrivilege.Generate();
        var fakeRolePrivilegeTwo = FakeRolePrivilege.Generate();
        var fakeRolePrivilegeThree = FakeRolePrivilege.Generate();
        var rolePrivilege = new List<RolePrivilege>();
        rolePrivilege.Add(fakeRolePrivilegeOne);
        rolePrivilege.Add(fakeRolePrivilegeTwo);
        rolePrivilege.Add(fakeRolePrivilegeThree);
        var mockDbData = rolePrivilege.AsQueryable().BuildMock();
        
        var queryParameters = new RolePrivilegeParametersDto() { PageSize = 1, PageNumber = 2 };

        _rolePrivilegeRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetRolePrivilegeList.Query(queryParameters);
        var handler = new GetRolePrivilegeList.Handler(_rolePrivilegeRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }
}