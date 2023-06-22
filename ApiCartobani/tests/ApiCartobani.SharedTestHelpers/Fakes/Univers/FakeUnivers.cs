namespace ApiCartobani.SharedTestHelpers.Fakes.Univers;

using AutoBogus;
using ApiCartobani.Domain.Univers;
using ApiCartobani.Domain.Univers.Dtos;

public sealed class FakeUnivers
{
    public static Univers Generate(UniversForCreationDto universForCreationDto)
    {
        return Univers.Create(universForCreationDto);
    }

    public static Univers Generate()
    {
        return Generate(new FakeUniversForCreationDto().Generate());
    }
}