namespace ApiCartobani.IntegrationTests.FeatureTests.Users;

using ApiCartobani.Domain.Users.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.User;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Users.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UserListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_user_list()
    {
        // Arrange
        var fakeUserOne = FakeUser.Generate(new FakeUserForCreationDto().Generate());
        var fakeUserTwo = FakeUser.Generate(new FakeUserForCreationDto().Generate());
        var queryParameters = new UserParametersDto();

        await InsertAsync(fakeUserOne, fakeUserTwo);

        // Act
        var query = new GetUserList.Query(queryParameters);
        var users = await SendAsync(query);

        // Assert
        users.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}