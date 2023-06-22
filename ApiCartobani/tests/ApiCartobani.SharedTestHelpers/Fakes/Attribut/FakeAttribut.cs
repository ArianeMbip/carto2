namespace ApiCartobani.SharedTestHelpers.Fakes.Attribut;

using AutoBogus;
using ApiCartobani.Domain.Attributs;
using ApiCartobani.Domain.Attributs.Dtos;

public sealed class FakeAttribut
{
    public static Attribut Generate(AttributForCreationDto attributForCreationDto)
    {
        return Attribut.Create(attributForCreationDto);
    }

    public static Attribut Generate()
    {
        return Generate(new FakeAttributForCreationDto().Generate());
    }
}