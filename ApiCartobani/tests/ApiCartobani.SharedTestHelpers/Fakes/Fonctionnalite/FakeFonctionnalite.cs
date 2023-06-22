namespace ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;

using AutoBogus;
using ApiCartobani.Domain.Fonctionnalites;
using ApiCartobani.Domain.Fonctionnalites.Dtos;

public sealed class FakeFonctionnalite
{
    public static Fonctionnalite Generate(FonctionnaliteForCreationDto fonctionnaliteForCreationDto)
    {
        return Fonctionnalite.Create(fonctionnaliteForCreationDto);
    }

    public static Fonctionnalite Generate()
    {
        return Generate(new FakeFonctionnaliteForCreationDto().Generate());
    }
}