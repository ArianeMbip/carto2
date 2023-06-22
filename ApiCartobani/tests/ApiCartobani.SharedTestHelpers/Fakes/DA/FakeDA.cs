namespace ApiCartobani.SharedTestHelpers.Fakes.DA;

using AutoBogus;
using ApiCartobani.Domain.DAs;
using ApiCartobani.Domain.DAs.Dtos;

public sealed class FakeDA
{
    public static DA Generate(DAForCreationDto dAForCreationDto)
    {
        return DA.Create(dAForCreationDto);
    }

    public static DA Generate()
    {
        return Generate(new FakeDAForCreationDto().Generate());
    }
}