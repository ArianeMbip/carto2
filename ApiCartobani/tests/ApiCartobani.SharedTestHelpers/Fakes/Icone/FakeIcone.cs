namespace ApiCartobani.SharedTestHelpers.Fakes.Icone;

using AutoBogus;
using ApiCartobani.Domain.Icones;
using ApiCartobani.Domain.Icones.Dtos;

public sealed class FakeIcone
{
    public static Icone Generate(IconeForCreationDto iconeForCreationDto)
    {
        return Icone.Create(iconeForCreationDto);
    }

    public static Icone Generate()
    {
        return Generate(new FakeIconeForCreationDto().Generate());
    }
}