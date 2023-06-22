namespace ApiCartobani.SharedTestHelpers.Fakes.Actif;

using AutoBogus;
using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.Actifs.Dtos;

public sealed class FakeActif
{
    public static Actif Generate(ActifForCreationDto actifForCreationDto)
    {
        return Actif.Create(actifForCreationDto);
    }

    public static Actif Generate()
    {
        return Generate(new FakeActifForCreationDto().Generate());
    }
}