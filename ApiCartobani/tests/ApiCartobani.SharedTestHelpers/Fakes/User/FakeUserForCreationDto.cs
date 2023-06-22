namespace ApiCartobani.SharedTestHelpers.Fakes.User;

using AutoBogus;
using ApiCartobani.Domain;
using ApiCartobani.Domain.Users.Dtos;
using ApiCartobani.Domain.Roles;

public sealed class FakeUserForCreationDto : AutoFaker<UserForCreationDto>
{
    public FakeUserForCreationDto()
    {
        RuleFor(u => u.Email, f => f.Person.Email);
    }
}