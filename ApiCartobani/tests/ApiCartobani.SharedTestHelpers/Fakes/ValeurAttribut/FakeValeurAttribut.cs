namespace ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;

using AutoBogus;
using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.ValeurAttributs.Dtos;

public sealed class FakeValeurAttribut
{
    public static ValeurAttribut Generate(ValeurAttributForCreationDto valeurAttributForCreationDto)
    {
        return ValeurAttribut.Create(valeurAttributForCreationDto);
    }

    public static ValeurAttribut Generate()
    {
        return Generate(new FakeValeurAttributForCreationDto().Generate());
    }
}