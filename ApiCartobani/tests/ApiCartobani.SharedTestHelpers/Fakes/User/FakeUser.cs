namespace ApiCartobani.SharedTestHelpers.Fakes.User;

using AutoBogus;
using ApiCartobani.Domain.Users;
using ApiCartobani.Domain.Users.Dtos;

public sealed class FakeUser
{
    public static User Generate(UserForCreationDto userForCreationDto)
    {
        return User.Create(userForCreationDto);
    }

    public static User Generate()
    {
        return Generate(new FakeUserForCreationDto().Generate());
    }
}